using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;


namespace DMSMVC.Service.Interface
{
    public interface IDocumentService
    {
        Task<BaseResponse<DocumentDTO>> CreateAsync(Staff staff, DocumentRequestModel request);
        Task<bool> DeleteDocument(string id);
        Task<BaseResponse<DocumentDTO>> SearchDocument(string title);
        //List<Document> ViewAllDocumentByDepartment(Staff staff
        Task<string> DownloadDocument(string id);
       
    }
}
