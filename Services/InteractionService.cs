using WardrobeBackendd.Model;
using WardrobeBackendd.Repositories;

namespace WardrobeBackendd.Services;

public class InteractionService
{
    private readonly IInteractionRepository _interactionRepository;

    public InteractionService(IInteractionRepository interactionRepository)
    {
        _interactionRepository = interactionRepository;
    }

    // Kombini keşfete yayınlar
    public async Task<bool> PublishCombineAsync(int combineId)
    {
        var combine = await _interactionRepository.GetCombineByIdAsync(combineId);
        if (combine == null)
            return false;

        combine.IsPublic = 1;
        await _interactionRepository.UpdateCombineAsync(combine);
        return true;
    }

    // Kombin görünürlüğünü aç/kapa yapar
    public async Task<bool> ToggleCombineVisibilityAsync(int combineId)
    {
        var combine = await _interactionRepository.GetCombineByIdAsync(combineId);
        if (combine == null)
            return false;

        combine.IsPublic = combine.IsPublic == 1 ? 0 : 1;
        await _interactionRepository.UpdateCombineAsync(combine);
        return true;
    }

    // Yayınlanmış kombinleri getirir
    public async Task<List<Combine>> GetPublicCombinesAsync()
    {
        return await _interactionRepository.GetPublicCombinesAsync();
    }

    // Beğeni ekler (kullanıcı daha önce beğenmediyse)
    public async Task LikeCombineAsync(int userId, int combineId)
    {
        var alreadyLiked = await _interactionRepository.HasUserLikedAsync(userId, combineId);
        if (!alreadyLiked)
        {
            await _interactionRepository.AddLikeAsync(new Like
            {
                UserId = userId,
                CombineId = combineId,
                CreatedAt = DateTime.Now
            });
        }
    }

    // Kombinin beğeni sayısını getirir
    public async Task<int> GetLikeCountAsync(int combineId)
    {
        return await _interactionRepository.GetLikeCountAsync(combineId);
    }

    // Kombine yorum ekler
    public async Task AddCommentAsync(int combineId, AddCommentDto dto)
    {
        await _interactionRepository.AddCommentAsync(new Comment
        {
            UserId = dto.UserId,
            CombineId = combineId,
            Text = dto.Text,
            CreatedAt = DateTime.Now
        });
    }

    // Kombine ait yorumları getirir
    public async Task<List<Comment>> GetCommentsAsync(int combineId)
    {
        return await _interactionRepository.GetCommentsByCombineIdAsync(combineId);
    }
}
