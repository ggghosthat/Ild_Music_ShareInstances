using ShareInstances.Instances;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ShareInstances.Stage;
public class PluginBag : IPluginBag
{
    private IDictionary<Guid, IPlayer> playerPlugins = new Dictionary<Guid, IPlayer>();
    private IDictionary<Guid, ICube> cubePlugins = new Dictionary<Guid, ICube>();

    private IPlayer currentPlayer = null;
    private ICube currentCube = null;

    public IPlayer CurrentPlayer => currentPlayer;
    public ICube CurrentCube => currentCube;


    public PluginBag(){}


    public void AddPlayerPlugin(IPlayer newPlayer)
    {
        if(!playerPlugins.ContainsKey(newPlayer.PlayerId))
            playerPlugins.Add(newPlayer.PlayerId, newPlayer);
    }

    public void AddCubePlugin(ICube newCube)
    {
        if(!cubePlugins.ContainsKey(newCube.CubeId))
            cubePlugins.Add(newCube.CubeId, newCube);
    }

    public async Task AddPlayerPluginsAsync(IEnumerable<IPlayer> players)
    {
        foreach(IPlayer player in players)
        {
            if(!playerPlugins.ContainsKey(player.PlayerId))
                playerPlugins.Add(player.PlayerId, player);
        }
    }
    
    public async Task AddCubePluginsAsync(IEnumerable<ICube> cubes)
    {
        foreach(ICube cube in cubes)
        {
            if(!cubePlugins.ContainsKey(cube.CubeId))
                cubePlugins.Add(cube.CubeId, cube);
        }
    }


    public void SetCurrentPlayer(Guid newPlayerId)
    {
        if(playerPlugins.ContainsKey(newPlayerId))
            currentPlayer = playerPlugins[newPlayerId];
    }

    public void SetCurrentCube(Guid newCubeId)
    {
        if(cubePlugins.ContainsKey(newCubeId))
            currentCube = cubePlugins[newCubeId];
    }


    public void DeletePlayer(Guid playerId)
    {
        if(playerPlugins.ContainsKey(playerId))
            playerPlugins.Remove(playerId);
    }

    public void DeleteCube(Guid cubeId)
    {
        if(cubePlugins.ContainsKey(cubeId))
            cubePlugins.Remove(cubeId);
    }

    public void ClearPlayers()
    {
        playerPlugins.Clear();        
    }

    public void ClearCubes()
    {
        cubePlugins.Clear();
    }
}
