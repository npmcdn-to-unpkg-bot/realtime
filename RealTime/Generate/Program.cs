namespace Generate
{
    using System;

    using Allors.Meta;

    class Program
    {
        static void Main(string[] args)
        {
            Generate();
        }

        public static void Generate()
        {
            Allors.Development.Repository.Tasks.Generate.Execute("../../../../Platform/Base/Templates/domain.cs.stg", "../../../Domain/Generated", Groups.Workspace);

            //Allors.Development.Repository.Tasks.Generate.Execute("../../../../Platform/Base/Templates/meta.ts.stg", "../../../Desktop/Allors/Client/Generated/meta", Groups.Workspace);
            //Allors.Development.Repository.Tasks.Generate.Execute("../../../../Platform/Base/Templates/domain.ts.stg", "../../../Desktop/Allors/Client/Generated/domain", Groups.Workspace);

            //Allors.Development.Repository.Tasks.Generate.Execute("../../../../Platform/Base/Templates/meta.ts.stg", "../../../Website/Allors/Client/Generated/meta", Groups.Workspace);
            //Allors.Development.Repository.Tasks.Generate.Execute("../../../../Platform/Base/Templates/domain.ts.stg", "../../../Website/Allors/Client/Generated/domain", Groups.Workspace);

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
