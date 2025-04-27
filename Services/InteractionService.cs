using WardrobeBackendd.Model;
using WardrobeBackendd.Repositories;

public class InteractionService
{
	private readonly IInteractionRepository _interactionRepository;

	public InteractionService(IInteractionRepository interactionRepository)
	{
		_interactionRepository = interactionRepository;
	}

	public async Task<bool> PublishCombineAsync(int combineId)
	{
		var combine = await _interactionRepository.GetCombineByIdAsync(combineId);
		if (combine == null)
			return false;

		combine.IsPublic = 1;
		await _interactionRepository.UpdateCombineAsync(combine);
		return true;
	}

	public async Task<bool> ToggleCombineVisibilityAsync(int combineId)
	{
		var combine = await _interactionRepository.GetCombineByIdAsync(combineId);
		if (combine == null)
			return false;

		combine.IsPublic = combine.IsPublic == 1 ? 0 : 1;
		await _interactionRepository.UpdateCombineAsync(combine);
		return true;
	}

	public async Task<List<CombineWithClothesDto>> GetPublicCombinesWithClothesAsync()
	{
		var publicCombines = await _interactionRepository.GetPublicCombinesAsync();
		var combineDtos = new List<CombineWithClothesDto>();

		var allClothes = await _interactionRepository.GetAllClothesAsync();
		var allUsers = await _interactionRepository.GetAllUsersAsync(); // 👈 Yeni eklenen kullanıcılar

		foreach (var combine in publicCombines)
		{
			var combineClothes = await _interactionRepository.GetCombineClothesByCombineIdAsync(combine.Id);
			var clothesIds = combineClothes.Select(cc => cc.ClothId).ToList();
			var clothes = allClothes.Where(cl => clothesIds.Contains(cl.Id)).ToList();

			var user = allUsers.FirstOrDefault(u => u.Id == combine.UserId); // 👈 User bilgisi

			combineDtos.Add(new CombineWithClothesDto
			{
				Id = combine.Id,
				UserId = combine.UserId,
				Name = combine.Name,
				CreatedAt = combine.CreatedAt,
				IsFavorite = combine.IsFavorite,
				IsPublic = combine.IsPublic,
				Clothes = clothes,
				UserFullName = user != null ? $"{user.Fullname}" : null,
				City = user?.City
			});
		}

		return combineDtos;
	}


	public async Task<List<Combine>> GetPublicCombinesAsync()
	{
		return await _interactionRepository.GetPublicCombinesAsync();
	}

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

	public async Task<int> GetLikeCountAsync(int combineId)
	{
		return await _interactionRepository.GetLikeCountAsync(combineId);
	}

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

	public async Task<List<Comment>> GetCommentsAsync(int combineId)
	{
		return await _interactionRepository.GetCommentsByCombineIdAsync(combineId);
	}
}
