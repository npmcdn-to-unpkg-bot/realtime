namespace Allors.Domain
{
					public interface Enumeration : UniquelyIdentifiable 
					{
									global::System.String Name {set;}

					}
					public interface ObjectState : UniquelyIdentifiable 
					{
									global::System.String Name {set;}

					}
					public interface UniquelyIdentifiable 
					{
									global::System.Guid UniqueId {set;}

					}
					public interface User 
					{
									global::System.String UserName {set;}


									global::System.String UserEmail {set;}

					}


					public interface Counter : UniquelyIdentifiable 
					{
					}
					public interface Media : UniquelyIdentifiable 
					{
									global::System.Guid? Revision {set;}


									global::System.String InDataUri {set;}

					}
					public interface Role : UniquelyIdentifiable 
					{
					}
					public interface UserGroup : UniquelyIdentifiable 
					{
					}
					public interface Person : User, UniquelyIdentifiable 
					{
									global::System.Boolean? IsOnline {set;}

					}

}