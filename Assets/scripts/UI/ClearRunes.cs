﻿public class ClearRunes : GuiBase
{
    //Removes the runes that the character has selected.
    public void OnClick()
    {
        if (UWCharacter.Instance != null)
        {
            //Debug.Log("Clearing Runes");
            UWCharacter.Instance.PlayerMagic.SetActiveRune(0, -1);
            UWCharacter.Instance.PlayerMagic.SetActiveRune(1, -1);
            UWCharacter.Instance.PlayerMagic.SetActiveRune(2, -1);
            ActiveRuneSlot.UpdateRuneSlots();
        }
    }
}
