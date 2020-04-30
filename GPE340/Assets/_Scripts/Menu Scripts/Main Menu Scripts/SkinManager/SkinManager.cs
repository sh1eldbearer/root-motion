using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager skinMgr;  // Singleton instance for the SkinManager

    [Tooltip("A array of objects containing information about the different skin colors available to players."), 
        SerializeField] private SkinColorDataStruct[] _skinColors = new SkinColorDataStruct[9];

    #region Public Properties
    /// <summary>
    /// An array of objects containing information about the different skin colors available to players.
    /// </summary>
    /// <returns>The array of ColorData objects containing information about the different skin colors available
    /// to players.</returns>
    public SkinColorDataStruct[] SkinColors
    {
        get { return _skinColors; }
    }
    #endregion

    // Awake is called before Start
    private void Awake()
    {
        // Makes the SkinManager a singleton and a persistent game object
        if (skinMgr == null)
        {
            skinMgr = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Gets the color value set for this skin color (used for UI displays).
    /// </summary>
    /// <param name="selectorIndex"></param>
    /// <returns>A color object representing the color assigned this skin color.</returns>
    public UnityEngine.Color GetRGBColor(int selectorIndex)
    {
        return _skinColors[selectorIndex].Color;
    }

    /// <summary>
    /// Assigns the materials for a designated skin color to an in-game character model.
    /// </summary>
    /// <param name="skinIndex">The index of the skin color to use for the character.</param>
    /// <param name="targetPawn">The GameObject of the character having its skin color changed.</param>
    public void AssignMaterials(int skinIndex, GameObject targetPawn)
    {
        AssignMaterials(skinIndex, targetPawn.GetComponentInChildren<PawnData>());
    }

    /// <summary>
    /// Assigns the materials for a designated skin color to an in-game character model.
    /// </summary>
    /// <param name="skinIndex">The index of the skin color to use for the character.</param>
    /// <param name="targetPawn">The PawnData component of the character having its skin color
    /// changed.</param>
    public void AssignMaterials(int skinIndex, PawnData targetPawn)
    {
        // Assigns the body material to the appropriate meshes
        foreach (SkinnedMeshRenderer bodyRenderer in targetPawn.BodyRenderers)
        {
            bodyRenderer.material = _skinColors[skinIndex].BodyMaterial;
        }

        // Assigns the brows material to the appropriate meshes
        foreach (SkinnedMeshRenderer browRenderer in targetPawn.BrowsRenderers)
        {
            browRenderer.material = _skinColors[skinIndex].BrowsMaterial;
        }

        // Assigns the eye material to the appropriate meshes
        foreach (SkinnedMeshRenderer browRenderer in targetPawn.EyeRenderers)
        {
            browRenderer.material = _skinColors[skinIndex].EyeMaterial;
        }

        // Assigns the eye spec material to the appropriate meshes
        foreach (SkinnedMeshRenderer eyeSpecRenderer in targetPawn.EyeSpecRenderers)
        {
            eyeSpecRenderer.material = _skinColors[skinIndex].EyeSpecMaterial;
        }
    }

    /// <summary>
    /// Assigns the avatar for a designated skin color to an in-game character model.
    /// </summary>
    /// <param name="skinIndex">The index of the skin color to use for the character.</param>
    /// <param name="targetPawn">The GameObject of the character having its avatar changed.</param>
    public void AssignAvatar(int skinIndex, GameObject targetPawn)
    {
        AssignAvatar(skinIndex, targetPawn.GetComponentInChildren<PawnData>());
    }

    /// <summary>
    /// Assigns the avatar for a designated skin color to an in-game character model.
    /// </summary>
    /// <param name="skinIndex">The index of the skin color to use for the character.</param>
    /// <param name="targetPawn">The PawnData component of the character of the character having
    /// its avatar changed.</param>
    public void AssignAvatar(int skinIndex, PawnData targetPawn)
    {
        targetPawn.PawnAnimator.avatar = _skinColors[skinIndex].Avatar;
    }
}
