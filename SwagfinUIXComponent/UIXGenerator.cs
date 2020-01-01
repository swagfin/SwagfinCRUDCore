using SwagfinCRUDCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagfinUIXComponent
{
    public class UIXGenerator
    {
        protected UIXPackage UIXTemplate {get;set;}
        protected string TableDir { get; set; }
        protected string SingleDir { get; set; }


        public UIXGenerator(UIXPackage InstalledUIXPackage)
        {
            this.UIXTemplate = InstalledUIXPackage;
            this.TableDir = InstalledUIXPackage.UIX_InstallDirectory + "\\table";
            this.SingleDir = InstalledUIXPackage.UIX_InstallDirectory + "\\single";
        }


        #region Get_GeneratedTables | List of Table With Columns
        public List<UIXTableDesign> Get_GeneratedUIXTablesTemplates(List<TableDesign> ListOfTablesWithItsColumns)
        {
            try
            {
                List<UIXTableDesign> uix_tables = new List<UIXTableDesign>();
                //#Loop Evert Table
                foreach (TableDesign table in ListOfTablesWithItsColumns)
                {
                    uix_tables.Add(new UIXTableDesign { TableName = table.Table_name, TableTemplates = this.Get_TableUIXTemplates(table) });
                }
                return uix_tables;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message); 
            }

        }
        #endregion


        #region Get_GeneratedSingle | List of Table With Columns
        public List<GeneratedUIXTemplate> Get_GeneratedUIXSinglesTemplates(List<TableDesign> ListOfTablesWithItsColumns)
        {
            try
            {
                /*
                 * This Code Below, Since the files inside will not be repeated per table
                 * 1. Will return List of GeneratedUIXTemplate Directly
                 * 2. The Below code gets all Sinlges and executes code starting with <x:foreach-table>
                 * For each table it loops and excutes the CodeBlock that may be inside
                 */
                List<GeneratedUIXTemplate> SingleTemplates = this.Get_UIXTemplates(this.SingleDir, "<x:foreach-table>", "</x:foreach-table>");
                //Loop all Then Execute as a Whole table Script
                foreach (GeneratedUIXTemplate template in SingleTemplates)
                {
                    template.ExecuteTableScript(ListOfTablesWithItsColumns);
                    //---->No Table Script | template.ExecuteTableScript(myTable);
                }
                return SingleTemplates; 
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }

        }
        #endregion

        //Protected
        #region Get_GeneratedTableModel | One Table With Columns
        //Tables UIX Templates require| Only Columns
        protected List<GeneratedUIXTemplate> Get_TableUIXTemplates(TableDesign CurrentTableWithColumns)
        {
            try
            {
                /*
                 * This Below Code will
                 * 1. Loop all files under Template Dir/tables/directory
                 * 2. Generate an UIX Template for each(This is because all files under (table) will be repeated for every table)
                 * 3. UIX Template Consists of: FileName,Sanitized UIX Design( Places with Code), LIST<ExecutableList>
                 * starting with <x:foreach-column> will be treated as executable code
                 * NOTE: Here, executable code will be replaced by a random no. e.g. {CODE:8955} and a similar code is saved by the code brackets
                 * inside each executable Code List 
                 */
                List<GeneratedUIXTemplate> TableTemplates = this.Get_UIXTemplates(this.TableDir, "<x:foreach-column>", "</x:foreach-column>");
                /*
                 * This Code Will Loop the Above Generated UIX Template and Execute Scripts if there is
                 * This Code will also Replace FileNames and Table Design Itself entities e.g. {table_name}
                 */
                foreach (GeneratedUIXTemplate template in TableTemplates)
                {
                    template.ExecuteTableColumnScript(CurrentTableWithColumns.Table_Columns);
                    template.ExecuteTableScript(CurrentTableWithColumns);
                }

                /*
                 * We Now have a Complete UIX Design Sanitized using the Template
                 */
                return TableTemplates; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        //Protected
        #region Get_UIXTemplates
        protected List<GeneratedUIXTemplate> Get_UIXTemplates(string TemplateBaseDirectory, string ExecutableStartAt, string ExecutableEndAt, string FilterOnlyWith = "*")
        {
            List<GeneratedUIXTemplate> UIX_TEMPLATES = new List<GeneratedUIXTemplate>();
            try
            {

                //#Loop all Columns]
                if (Directory.Exists(TemplateBaseDirectory))
                {
                    foreach (string filename in Directory.GetFiles(TemplateBaseDirectory, FilterOnlyWith, SearchOption.AllDirectories))
                    {
                        string DataContents = File.ReadAllText(filename);
                        string NewFileName = Path.GetFileName(filename);
                        string CleanedCode = "";
                        UIX_TEMPLATES.Add(new GeneratedUIXTemplate
                        {
                            Original_FilePath = filename,
                            SanitizedFileName = NewFileName.Replace("{dir}", "\\"),
                            OriginalUIXTemplate = DataContents,
                            ExecutableScript = Get_CodeBlocksBtwn(filename, ExecutableStartAt, ExecutableEndAt, ref CleanedCode),
                            SanitizedUIXDesign = CleanedCode,
                            DateTimeGenerated = DateTime.Now,
                            GeneratedByTemplate =this.UIXTemplate.UIX_Name 
                        }
                        );
                    }
                }


            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            return UIX_TEMPLATES;
        }

        #endregion
        //Protected
        #region Get_CodeBlocksBtwn
        protected List<ExecutableBlock> Get_CodeBlocksBtwn(string FilePathstring, string StartingSyntax, string EndingSyntax, ref string CleanedCode)
        {
            List<ExecutableBlock> Exeutables = new List<ExecutableBlock>();
            try
            {
                CleanedCode = "";
                string executable_string = "";
                bool started_code = false;
                int start_at = 0;
                int current_line = 0;
                var lines = File.ReadLines(FilePathstring);
                foreach (var line in lines)
                {
                    if (line.Contains(StartingSyntax.Trim()))
                    {
                        started_code = true;
                        start_at = current_line;
                        executable_string = "";
                    }
                    else if (line.Contains(EndingSyntax.Trim()) && started_code)
                    {
                        started_code = false;
                        string replce_code = "{UIX_CODE_" + start_at.ToString() + current_line.ToString() + "}";
                        CleanedCode += replce_code + Environment.NewLine;
                        Exeutables.Add(new ExecutableBlock { CodeInside = executable_string, Code_start_at = start_at, Code_end_at = current_line, ReplaceParam = replce_code });
                    }
                    else if (started_code)
                    {
                        executable_string += line + Environment.NewLine;
                    }
                    else
                    {
                        CleanedCode += line + Environment.NewLine;
                    }

                    //Increment Line
                    current_line += 1;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            return Exeutables;
        }

        #endregion
    }
}
