using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace RectangleSpreadController.Models
{
    public class CutLineAndSheet
    {
        public ICutLine Line { get;}

        public Sheet Sheet { get;}

        public ISheet[] OutputSheets  { get;}

    private CutLineAndSheet(ICutLine line, Sheet sheet, ISheet[] outputSheets)
        {
            Line = line;
            Sheet = sheet;
            OutputSheets = outputSheets;
        }

        public static CutLineAndSheet Create(ICutLine line, Sheet sheet, ISheet[] outputSheets)
        {
            return new CutLineAndSheet(line, sheet, outputSheets);
        }

    }
}
