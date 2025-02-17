using UnityEngine;

[CreateAssetMenu(fileName = "CharacterShopDatabase", menuName = "Shoping/Character shop database")]
public class CharacterShopDatabase : ScriptableObject
{
    public Character[] characters;

    public int CharactersCount
    {
        get { return characters.Length; }
    }
    
    public Character GetCharacter(int index)
    {
        return characters[index];
    }
    
    public void purchaseCharacter(int index)
    {
        characters[index].isPurchased = true;
    }
}
