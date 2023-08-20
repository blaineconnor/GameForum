namespace Game.Forum.Domain.Common
{
    public interface IHasUpdatedAt
    {
        DateTime? UpdatedTime { get; set; }
    }
}
