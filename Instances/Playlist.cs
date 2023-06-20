using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace ShareInstances.Instances;
public struct Playlist
{
	public Guid Id {get; init;} = Guid.NewGuid();
	public ReadOnlyMemory<char> Name {get; private set;} = string.Empty.AsMemory(); 
	public ReadOnlyMemory<char> Description {get; private set;} = string.Empty.AsMemory();
    public ReadOnlyMemory<char> AvatarBase64 {get; private set;} = string.Empty.AsMemory(); 

    //Use for user space playing
    private Lazy<List<Track>> Tracks; 

    //Please, be carefull when you call this property and DO NOT call much this property
    //When playlist contains many track objects, Lazy<T> will init whole list in CLR's heap
    public int Count => Tracks.Value.Count;
	
    public int CurrentIndex {get; set;} = 0;
	public bool IsOrdered { get; private set; } = false;

    public Track this[int i]
    {
        get
        {
            return Tracks.Value[i];
        }
    }

    public Playlist(ReadOnlyMemory<char> name, 
                    ReadOnlyMemory<char> description,
                    ReadOnlyMemory<char> avatarPath)
    {
        Name = name;
        Description = description;
        AvatarBase64 = avatarPath;

        Tracks = new Lazy<List<Track>>();
    }

    #region Collection Manipulation Methods
    public void AddTrack(Track track)
    {        
    	Tracks.Value.Add(track);
    }

    public void AddTrackRange(IList<Track> tracks)
    {
        Tracks.Value.AddRange(tracks);
    }

    public void RemoveTrack(Track track)
    {        
    	if(Tracks.Value.Contains(track))
    	{
    		Tracks.Value.Remove(track);
    	}
    }

    public void Clear()
    {
        Tracks.Value.Clear();
    }
    #endregion


    #region Avatar Manipulation
    public byte[] GetAvatar()
    {
        try
        {
            byte[] result;
            return Convert.FromBase64String(AvatarBase64.ToString());
        }
        catch(Exception ex)
        {
            //Speciall logging or throwing logic
            return null;
        }
    }

    public string SetAvatar(string path)
    {
        if(path is not null && File.Exists(path))
        {
            try
            {
                byte[] file = System.IO.File.ReadAllBytes(path);
                return Convert.ToBase64String(file); 
            }
            catch(Exception ex)
            {
                //Speciall logging or throwing logic
                throw ex;
            }            
        }
        else return null;
    }
    #endregion
}
