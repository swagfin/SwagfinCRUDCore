using SwagfinCRUDCore;
using System;
using System.Collections.Generic;

namespace SwagfinUIXComponent
{
  public class GeneratedUIXTemplate
    {
        public string Original_FilePath { get; set; }
        public string SanitizedFileName { get; set; }
        public string OriginalUIXTemplate { get; set; }
        public string SanitizedUIXDesign { get; set; }
        public List<ExecutableBlock> ExecutableScript { get; set; }
        public DateTime DateTimeGenerated { get; set; }
        public string GeneratedByTemplate { get; set; }

        #region ExecuteTableColumnScript
        public bool ExecuteTableColumnScript(List<TableColumn> TableColumns)
        {
            try
            {
                foreach (ExecutableBlock codeBlock in this.ExecutableScript)
                {
                    ColumnDataTypeDesigner eeDesign = new ColumnDataTypeDesigner(codeBlock.CodeInside);
                    string All_Values = "";
                    /*
                     * Custom Width and Locations Parameters
                     * DEPRECATING..... This Location Getter wil soon be replaced
                     */
                    int label_count_space = 0;
                    int TabCount = 0;
                    int SideSkipleft = 0;
                   //Location 3 is Global
                    string location3 = "0,0";
                    foreach (TableColumn column in TableColumns)
                    {
                        TabCount += 1;
                        string location1 = "0,0";
                        string location2 = "0,0";
                        //Get SideSkip Left this Code will Soon be replaces with something else
                        if (TabCount <=5) { SideSkipleft = 55; } else if(TabCount <=10) { SideSkipleft = 290; }else if(TabCount <= 15) { SideSkipleft = 290 + 235; }
                        else if(TabCount <= 20){ SideSkipleft = 525 + 235; } else if (TabCount <= 25) { SideSkipleft = 760 + 235; } else { SideSkipleft = 995 + 235; }
                        //Get Label Count Space
                        if(label_count_space < 350) { label_count_space += 35; } else if(label_count_space >= 350) { label_count_space = 0; label_count_space += 35; }
                        //Custom Location Now
                        location1 = SideSkipleft.ToString() + ", " + label_count_space;
                        //then Increment
                        label_count_space += 35;
                        location2 = SideSkipleft.ToString() + ", " + label_count_space;
                      
                        string new_Val = "";
                        //@Check Reference Key
                        if (string.IsNullOrEmpty(column.Referenced_table_name) == false && string.IsNullOrEmpty(column.Referenced_column_name) == false)
                            new_Val = eeDesign.Referenced_design;
                        else if (column.Extra == "auto_increment")
                            new_Val = eeDesign.Auto;
                        else if (column.Data_type == "int")
                            new_Val = eeDesign.Int_design;
                        else if (column.Data_type == "datetime" | column.Data_type == "timestamp" | column.Data_type == "time")
                            new_Val = eeDesign.Datetime_design;
                        else if (column.Data_type == "double")
                            new_Val = eeDesign.Double_design;
                        else if (column.Data_type == "decimal")
                            new_Val = eeDesign.Decimal_design;
                        else if (column.Data_type == "float")
                            new_Val = eeDesign.Float_design;
                        else if (column.Data_type == "date" | column.Data_type == "year")
                            new_Val = eeDesign.Date_design;
                        else
                            new_Val = eeDesign.Varchar_design;

                        //#Sanitize
                        column.SanitizeDataMap(ref new_Val);
                        //TabIndex | Still in all Version {tab_index}
                        new_Val = new_Val.Replace("{tab_index}", TabCount.ToString());
                        //Replace Locations For Looping Columns {Deprecated}
                        new_Val = new_Val.Replace("{location1}", location1);
                        new_Val = new_Val.Replace("{location2}", location2);
                        new_Val = new_Val.Replace("{location3}", location3);
                        All_Values += new_Val;

                    }
                    //@Finally Replace
                    this.SanitizedUIXDesign = this.SanitizedUIXDesign.Replace(codeBlock.ReplaceParam, All_Values);
                    //TabIndex | Still in all Version {tab_index}
                    this.SanitizedUIXDesign = this.SanitizedUIXDesign.Replace("{tab_index}", TabCount.ToString());

                    //{DEPRECATING....}Other Components Replace
                    label_count_space += 35;
                    location3 = SideSkipleft.ToString() + ", " + label_count_space;
                    this.SanitizedUIXDesign = this.SanitizedUIXDesign.Replace("{location3}", location3);
                    //Window Location Replace
                    //#Get New Form SizeWidth Due to affected
                    int form_width = 820;
                    try
                    {
                        decimal get_rows = (TableColumns.Count / 5);
                        get_rows = Math.Ceiling(get_rows);
                        decimal widthh = (55 + (235 * get_rows));
                        widthh = Math.Ceiling(widthh);
                        form_width = (int)widthh;
                        //#Send Replace it Exits {form_width}
                    }
                    catch (Exception)
                    {

                    }
                    //Will Soon {Deprecating Setting for Setting Custom Window Width}
                    this.SanitizedUIXDesign = this.SanitizedUIXDesign.Replace("{form_width}", form_width.ToString());

                }


                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        #endregion

        #region ExecuteTableScript
        public bool ExecuteTableScript(TableDesign TableDesign)
        {
            try
            {
                foreach (ExecutableBlock codeBlock in this.ExecutableScript)
                {
                    //@Finally Replace
                    this.SanitizedUIXDesign = this.SanitizedUIXDesign.Replace(codeBlock.ReplaceParam, codeBlock.CodeInside);
                }
                //Sanitize Every Property
                //#UIX Sanitized Design
                string all_data = this.SanitizedUIXDesign;
                TableDesign.SanitizeDataMap(ref all_data);
                this.SanitizedUIXDesign = all_data;
                //#Sanitized Name
                string new_name = this.SanitizedFileName;
                TableDesign.SanitizeDataMap(ref new_name);
                this.SanitizedFileName = new_name;
                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
        #endregion

        #region ExecuteTableScript | All Tables
        public bool ExecuteTableScript(List<TableDesign> TableDesigns)
        {
            try
            {  
                foreach (ExecutableBlock codeBlock in this.ExecutableScript)
                {
                    //#Loop All Tables         
                    string copy_of_CodeInside = codeBlock.CodeInside;
                    string final_table_data = "";
                    foreach (TableDesign table in TableDesigns)
                    {
                        //#Sanitize
                        string new_design = copy_of_CodeInside;
                        table.SanitizeDataMap(ref new_design);
                        final_table_data += new_design; 
                    }

                    //@Finally Replace
                    this.SanitizedUIXDesign = this.SanitizedUIXDesign.Replace(codeBlock.ReplaceParam, final_table_data);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        #endregion


    }
}
