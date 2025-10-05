using ClosedXML.Excel;
using FormManagementSystem.Repositories;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FormManagementSystem.Services
{
    public class ExcelExportService : IExcelExportService
    {
        private readonly IUnitOfWork _uow;
        public ExcelExportService(IUnitOfWork uow) => _uow = uow;

        public async Task<MemoryStream> ExportFormAsync(int formId)
        {
            var form = await _uow.Forms.GetByIdWithFieldsAsync(formId);
            if (form == null) throw new InvalidOperationException("Form not found.");

            var submissions = (await _uow.Submissions.GetByFormIdAsync(formId)).ToList();

            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add(form.Name ?? $"Form-{formId}");

            var fields = form.Fields.OrderBy(f => f.RowNumber).ThenBy(f => f.SortOrder).ToList();

            int col = 1;
            ws.Cell(1, col++).Value = "SubmissionId";
            ws.Cell(1, col++).Value = "SubmittedBy";
            ws.Cell(1, col++).Value = "SubmittedAt";

            foreach (var f in fields) ws.Cell(1, col++).Value = f.Label;

            int row = 2;
            foreach (var s in submissions)
            {
                col = 1;
                ws.Cell(row, col++).Value = s.FormSubmissionId;
                ws.Cell(row, col++).Value = s.SubmittedById;
                ws.Cell(row, col++).Value = s.SubmittedAt.ToString("u");

                var dict = s.FieldSubmissions.ToDictionary(v => v.FormFieldId, v => v.Value);
                foreach (var f in fields)
                {
                    dict.TryGetValue(f.FormFieldId, out var val);
                    ws.Cell(row, col++).Value = val;
                }
                row++;
            }

            var ms = new MemoryStream();
            wb.SaveAs(ms);
            ms.Position = 0;
            return ms;
        }
    }
}
