using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSkinService : MonoBehaviour {

    // The shortest list wins
    public List<Color> m_playerColors;
    public List<string> m_playerNames;

    class PlayerSkin
    {
        public PlayerSkin(string p_name, Color p_color)
        {
            name = p_name;
            color = p_color;
        }
        public string name = "";
        public Color color;
        public bool isAvailable = true;
        public string owner = "";
    }

    SortedList<string, PlayerSkin> m_playerSkinList = new SortedList<string, PlayerSkin>();

	void Start ()
    {
        int smallestCount = m_playerColors.Count >= m_playerNames.Count ? m_playerNames.Count : m_playerColors.Count;
        for (int i = 0; i < smallestCount; ++i)
        {
            m_playerSkinList.Add(m_playerNames[i], new PlayerSkin(m_playerNames[i], m_playerColors[i]));
        }
    }

    public Color AssignNextSkinWithName(PlayerController player, string name)
    {
        PlayerSkin skin;
        bool playerHasName = m_playerSkinList.TryGetValue(player.PlayerName, out skin);
        if (skin != null)
        {
            skin.isAvailable = true;

            skin = m_playerSkinList[GetNextSkinName(player.PlayerName)];
            player.PlayerName = skin.name;
            player.GetComponent<SpriteRenderer>().color = skin.color;
            skin.isAvailable = false;
            return skin.color;
        }
        else
        {
            foreach (string availableName in m_playerSkinList.Keys)
            {
                skin = m_playerSkinList[availableName];
                if (skin.isAvailable)
                {
                    player.PlayerName = skin.name;
                    player.GetComponent<SpriteRenderer>().color = skin.color;
                    skin.isAvailable = false;
                    return skin.color;
                }
            }
            return Color.white;
        }
    }

    string GetNextSkinName(string currentSkinName)
    {
        string nextSkinName = currentSkinName;
        int nameIndex = m_playerNames.IndexOf(currentSkinName);
        if(nameIndex == -1 || nameIndex == m_playerNames.Count - 1)
        {
            nextSkinName = m_playerSkinList[m_playerNames[0]].isAvailable ? m_playerNames[0] : m_playerNames[1];
        }else
        {
            nextSkinName = m_playerSkinList[m_playerNames[nameIndex + 1 ]].isAvailable ? m_playerNames[nameIndex + 1] : m_playerNames[nameIndex + 2];
        }
        return nextSkinName;
    }

    // TODO : faire comme GetNextSkinName, mais changer les index
    string GetPreviousSkinName(string currentSkinName)
    {
        return GetNextSkinName(currentSkinName);
    }
}
