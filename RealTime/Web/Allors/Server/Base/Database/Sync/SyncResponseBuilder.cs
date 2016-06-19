﻿namespace Allors.Web.Database
{
    using System.Collections.Generic;
    using System.Linq;
    using Allors.Domain;
    using Allors.Meta;

    public class SyncResponseBuilder
    {
        private static readonly object[][] EmptyRoles = { };
        private static readonly object[][] EmptyMethods = { };

        private readonly ISession session;
        private readonly SyncRequest syncRequest;
        private readonly string @group;
        private readonly User user;

        public SyncResponseBuilder(ISession session, User user, SyncRequest syncRequest, string @group)
        {
            this.session = session;
            this.user = user;
            this.syncRequest = syncRequest;
            this.group = group;
        }

        public SyncResponse Build()
        {
            var objects = this.session.Instantiate(this.syncRequest.Objects);

            // Prefetch
            var objectByClass = objects.GroupBy(v => v.Strategy.Class, v => v);
            foreach (var groupBy in objectByClass)
            {
                var prefetchClass = (Class)groupBy.Key;
                var prefetchObjects = groupBy.ToArray();

                var prefetchPolicyBuilder = new PrefetchPolicyBuilder();
                prefetchPolicyBuilder.WithGroupRules(prefetchClass, this.@group);
                prefetchPolicyBuilder.WithSecurityRules();
                var prefetcher = prefetchPolicyBuilder.Build();

                this.session.Prefetch(prefetcher, prefetchObjects);
            }

            return new SyncResponse
            {
                UserSecurityHash = this.user.SecurityHash(),
                Objects = objects.Select(x => new SyncResponseObject
                {
                    I = x.Id.ToString(),
                    V = x.Strategy.ObjectVersion.ToString(),
                    T = x.Strategy.Class.Name,
                    Roles = this.GetRoles(x),
                    Methods = this.GetMethods(x),
                }).ToArray() 
            };
        }

        private object[][] GetRoles(IObject obj)
        {
            var composite = (Composite)obj.Strategy.Class;

            IList<RoleType> roleTypes;
            if (composite.RoleTypesByGroup.TryGetValue(this.@group, out roleTypes))
            {
                if (roleTypes.Count > 0)
                {
                    AccessControlList acl = null;
                    if (obj is AccessControlledObject)
                    {
                        acl = new AccessControlList(obj, this.user);
                    }

                    var roles = new List<object[]>();
                    foreach (var roleType in roleTypes)
                    {
                        var propertyName = roleType.PropertyName;

                        var canRead = acl == null || acl.CanRead(roleType);
                        var canWrite = acl != null && acl.CanWrite(roleType);
                        var access = (canRead ? "r" : string.Empty) + (canWrite ? "w" : string.Empty);

                        if (canRead)
                        {
                            if (roleType.ObjectType.IsUnit)
                            {
                                var role = obj.Strategy.GetUnitRole(roleType.RelationType);
                                if (role != null)
                                {
                                    roles.Add(new[] { propertyName, access, role });
                                }
                                else
                                {
                                    roles.Add(new object[] { propertyName, access });
                                }
                            }
                            else
                            {
                                if (roleType.IsOne)
                                {
                                    var role = obj.Strategy.GetCompositeRole(roleType.RelationType);
                                    if (role != null)
                                    {
                                        roles.Add(new object[] { propertyName, access, role.Id.ToString() });
                                    }
                                    else
                                    {
                                        roles.Add(new object[] { propertyName, access });
                                    }
                                }
                                else
                                {
                                    var role = obj.Strategy.GetCompositeRoles(roleType.RelationType);
                                    if (role.Count != 0)
                                    {
                                        var ids = role.Cast<IObject>().Select(roleObject => roleObject.Id.ToString()).ToList();
                                        roles.Add(new object[] { propertyName, access, ids });
                                    }
                                    else
                                    {
                                        roles.Add(new object[] { propertyName, access });
                                    }
                                }
                            }
                        }
                        else
                        {
                            roles.Add(new object[] { propertyName, access });
                        }
                    }

                    return roles.ToArray();
                }
            }

            return EmptyRoles;
        }

        private object[][] GetMethods(IObject obj)
        {
            var composite = (Composite)obj.Strategy.Class;

            IList<MethodType> methodTypes;
            if (composite.MethodTypesByGroup.TryGetValue(this.@group, out methodTypes))
            {
                if (methodTypes.Count > 0)
                {
                    AccessControlList acl = null;
                    if (obj is AccessControlledObject)
                    {
                        acl = new AccessControlList(obj, this.user);
                    }

                    var methods = new List<object[]>();
                    foreach (var methodType in methodTypes)
                    {
                        var methodName = methodType.Name;

                        var canExecute = acl == null || acl.CanExecute(methodType);
                        var access = canExecute ? "x" : string.Empty;

                        methods.Add(new object[] { methodName, access });
                    }

                    return methods.ToArray();
                }
            }

            return EmptyMethods;
        }
    }
}