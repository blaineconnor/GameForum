namespace Game.Forum.Domain.Common

{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public BaseEntity()
        {
            CreateDate = DateTime.UtcNow;
        }
        public void Update()
        {
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
