using System.IO;
using System.Threading.Tasks;

namespace FormManagementSystem.Services
{
    public interface IExcelExportService
    {
        Task<MemoryStream> ExportFormAsync(int formId);
    }
}
