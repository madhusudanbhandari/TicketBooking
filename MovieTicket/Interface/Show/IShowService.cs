using MovieTicket.Dtos;

namespace MovieTicket.Interface;

public interface IShowService
{
    public Task<ViewShowDto> AddShowAsync(AddShowDto dto);
    public Task<List<ViewShowDto>> ViewAllShowsAsync();
    public Task<ViewShowDto?> ViewShowByIdAsync(int id);
    public Task<ViewShowDto?> UpdateShowAsync(int id, UpdateShowDto dto);
    public Task<string?> DeleteShowAsync(int id);
}