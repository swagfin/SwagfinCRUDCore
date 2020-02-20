using SwagfinCRUDCore.InstalledModelGenerators;
using System.Collections.Generic;

namespace SwagfinCRUDCore
{
    public class SupportedEngine
    {
        public string Engine_Key { get; set; }
        public string Engine_Name { get; set; }
        public string Engine_Info { get; set; }
        public ILanguageModelGenerator ModelGenerator { get; set; }
        public string ModelSaveExtension { get; set; }
        public string ModelSaveSubFolder { get; set; } = "Models\\";
        public List<SupportedEngineDependancy> Dependancies { get; set; }

        #region Get_SupportedEngines | Registering Supported Engine
        //-->Get SupportedEngines

        public List<SupportedEngine> Get_SupportedEngines()
        {
            return new List<SupportedEngine>
            {
                //-->>MySQL for VB.NET
                new SupportedEngine
                {
                    Engine_Key = "CONLYENTITIES",
                    Engine_Name = "C# Entities/Model Classes Generator",
                    Engine_Info = "Generate Entity classes from the Database",
                    ModelSaveExtension=".cs",
                    ModelSaveSubFolder="Entity\\",
                    ModelGenerator = new CSharpPlainEntityGenerator(),
                    Dependancies = new List<SupportedEngineDependancy>()
                },
                new SupportedEngine
                {
                    Engine_Key = "CONLYSERVICES",
                    Engine_Name = "C# Interface/Repository Services Generator",
                    Engine_Info = "Generate Interface/Repository/Services classes from Database to help you loose couple your system logics",
                    ModelSaveExtension="Service.cs",
                    ModelSaveSubFolder="Services\\I",
                    ModelGenerator = new CSharpServicesGenerator(),
                    Dependancies = new List<SupportedEngineDependancy>()
                },
                new SupportedEngine
                {
                    Engine_Key = "CONLYSERVICESIMPLEMENTATION",
                    Engine_Name = "C# Interface/Repository Services Implementation Generator",
                    Engine_Info = "Generate Implementations for the interface repository that you have already generated from your database.",
                    ModelSaveExtension="Service.cs",
                    ModelSaveSubFolder="Services\\Implementations\\",
                    ModelGenerator = new CSharpServicesImplementationGenerator(),
                    Dependancies = new List<SupportedEngineDependancy>()
                },
                new SupportedEngine
                {
                    Engine_Key = "CONLYSERVICESIMPLEMENTATIONEFCORE",
                    Engine_Name = "C# Entity Framework Interface/Repository Services Implementation (.NET Core) Generator",
                    Engine_Info = "Generate Entity Framework Implementations for the interface repository that you have already generated for .NET Core.",
                    ModelSaveExtension="Service.cs",
                    ModelSaveSubFolder="Services\\Implementations\\",
                    ModelGenerator = new CSharpServicesImpEFCoreGenerator(),
                    Dependancies = new List<SupportedEngineDependancy>()
                },
                new SupportedEngine
                {
                    Engine_Key = "CONLYSERVICESIMPLEMENTATIONEF",
                    Engine_Name = "C# Entity Framework Interface/Repository Services Implementation (.NET Framework) Generator",
                    Engine_Info = "Generate Entity Framework Implementations for the interface repository that you have already generated for .NET Framework.",
                    ModelSaveExtension="Service.cs",
                    ModelSaveSubFolder="Services\\Implementations\\",
                    ModelGenerator = new CsharpServicesImpEFGenerator(),
                    Dependancies = new List<SupportedEngineDependancy>()
                },

                new SupportedEngine
                {
                    Engine_Key = "MYSQL",
                    Engine_Name = "MySQL for VB.NET Code Engine Generator",
                    Engine_Info = "Generator Engine that supports MySQL for VB.NET Language.",
                    ModelSaveExtension=".vb",
                    ModelGenerator = new Vb_MySQL_Generator(),
                    Dependancies = new List<SupportedEngineDependancy>()
                },

                                //-->>MySQL for C#
                new SupportedEngine
                {
                    Engine_Key = "MYSQL",
                    Engine_Name = "MySQL for C# Code Engine Generator",
                    Engine_Info = "Generator Engine that supports MySQL for C# Language.",
                    ModelSaveExtension=".cs",
                    ModelGenerator = new Csharp_MySQL_Generator(),
                    Dependancies = new List<SupportedEngineDependancy>()
                },

                                //-->>Microsoft SQL for Vb.net
                new SupportedEngine
                {
                    Engine_Key = "MSSQL",
                    Engine_Name = "Microsoft SQL for VB.NET Code Engine Generator",
                    Engine_Info = "Generator Engine that supports Microsoft SQL for VB.NET Language.",
                    ModelSaveExtension=".vb",
                    ModelGenerator = new Vb_MSSQL_Generator(),
                    Dependancies = new List<SupportedEngineDependancy>()
                },

                    //-->>Microsoft SQL for C#
                new SupportedEngine
                {
                    Engine_Key = "MSSQL",
                    Engine_Name = "Microsoft SQL for C# Code Engine Generator",
                    Engine_Info = "Generator Engine that supports Microsoft SQL for C# Language.",
                    ModelSaveExtension=".cs",
                     ModelGenerator = new Csharp_MSSQL_Generator(),
                    Dependancies = new List<SupportedEngineDependancy>()
                },

                                //-->>SQLITE SQL for Vb.net
                new SupportedEngine
                {
                    Engine_Key = "SQLITE",
                    Engine_Name = "SQLite DB for VB.NET Code Engine Generator",
                    Engine_Info = "Generator Engine that supports SQLite Database for VB.NET Language.",
                    ModelSaveExtension=".vb",
                    ModelGenerator = new Vb_SQLITE_Generator(),
                    Dependancies = new List<SupportedEngineDependancy>()
                },

                    //-->>SQLite C#
                new SupportedEngine
                {
                    Engine_Key = "SQLITE",
                    Engine_Name = "SQLite SQL for C# Code Engine Generator",
                    Engine_Info = "Generator Engine that supports Microsoft SQL for C# Language.",
                    ModelSaveExtension=".cs",
                     ModelGenerator = new Csharp_SQLITE_Generator(),
                    Dependancies = new List<SupportedEngineDependancy>()
                },


                                //-->>SQLITE SQL for Vb.net
                new SupportedEngine
                {
                    Engine_Key = "ACCESSDB",
                    Engine_Name = "Microsoft Access DB for VB.NET Code Engine Generator",
                    Engine_Info = "Generator Engine that supports Microsoft Access Database for VB.NET Language.",
                    ModelSaveExtension=".vb",
                    ModelGenerator = new Vb_ACCESSDB_Generator(),
                    Dependancies = new List<SupportedEngineDependancy>()
                },

                    //-->>SQLite C#
                new SupportedEngine
                {
                    Engine_Key = "ACCESSDB",
                    Engine_Name = "Microsoft Access DB for C# Code Engine Generator",
                    Engine_Info = "Generator Engine that supports Microsoft Access Database for C# Language.",
                    ModelSaveExtension=".cs",
                    ModelGenerator = new Csharp_ACCESSDB_Generator(),
                    Dependancies = new List<SupportedEngineDependancy>()
                },

                new SupportedEngine
                {
                    Engine_Key = "EMPTY",
                    Engine_Name = "Use an Empty Model Generator",
                    Engine_Info = "Usefull in senarios where you only need to Generate UIX Designs without Models.",
                    ModelSaveExtension=".txt",
                    ModelGenerator = new EmptyModelGenerator(),
                    Dependancies = new List<SupportedEngineDependancy>()
                },

            };
        }

        #endregion

    }

}
