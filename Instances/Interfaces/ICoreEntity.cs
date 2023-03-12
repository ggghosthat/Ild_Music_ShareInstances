using System;

namespace ShareInstances.Instances.Interfaces
{
    public interface ICoreEntity
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AvatarBase64 {get; set;}
        public void SetAvatar(string path);
    }
}