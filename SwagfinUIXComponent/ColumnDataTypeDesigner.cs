using System;

namespace SwagfinUIXComponent
{
    class ColumnDataTypeDesigner
    {
        public string Auto { get; set; }
        public string Int_design { get; set; }
        public string Datetime_design { get; set; }
        public string Double_design { get; set; }
        public string Decimal_design { get; set; }
        public string Float_design { get; set; }
        public string Date_design { get; set; }
        public string Varchar_design { get; set; }
        //#New Version
        public string Referenced_design { get; set; }


        public ColumnDataTypeDesigner(string CodeBlock)
        {
            this.Auto = this.FindCodeBlockIn(CodeBlock, "<x:auto>", "</x:auto>");
            this.Int_design = this.FindCodeBlockIn(CodeBlock, "<x:int>", "</x:int>");
            this.Datetime_design = this.FindCodeBlockIn(CodeBlock, "<x:datetime>", "</x:datetime>");
            this.Double_design = this.FindCodeBlockIn(CodeBlock, "<x:double>", "</x:double>");
            this.Decimal_design = this.FindCodeBlockIn(CodeBlock, "<x:decimal>", "</x:decimal>");
            this.Float_design = this.FindCodeBlockIn(CodeBlock, "<x:float>", "</x:float>");
            this.Date_design = this.FindCodeBlockIn(CodeBlock, "<x:date>", "</x:date>");
            this.Varchar_design = this.FindCodeBlockIn(CodeBlock, "<x:varchar>", "</x:varchar>");
            this.Referenced_design = this.FindCodeBlockIn(CodeBlock, "<x:referenced>", "</x:referenced>");

        }

        #region FindCodeBlockIn
        protected string FindCodeBlockIn(string CodeBlock, string StartTag, string EndTag)
        {
            string New_CodeBlock = "";
            try
            {
                //#Check Without wast of Time
                if (CodeBlock.Contains(StartTag.Trim()) == false && CodeBlock.Contains(StartTag.Trim()) == false)
                    return New_CodeBlock;

                string[] delimiter = { Environment.NewLine };
                string[] b = CodeBlock.Split(delimiter, StringSplitOptions.None);
                bool started_code = false;
                //#For each
                foreach (string curLine in b)
                {
                    if (curLine.Contains(StartTag.Trim()))
                    {
                        started_code = true;
                        New_CodeBlock = "";
                    }
                    else if (curLine.Contains(EndTag.Trim()) && started_code)
                    {
                        started_code = false;
                    }
                    else if (started_code)
                    {
                        New_CodeBlock += curLine + Environment.NewLine;
                    }

                }

            }
            catch (Exception)
            {

            }
            return New_CodeBlock;
        }

        #endregion

    }
}
