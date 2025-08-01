using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform[] levels;
    public int activeLevelIndex = 0;

    public float screenWidth = 14.4f;

    public Transform GetLevel(int index)
    {
        if (index >= 0 && index < levels.Length)
            return levels[index];
        return null;
    }

    public int GetNextIndex(bool forward)
    {
        return forward ? activeLevelIndex + 1 : activeLevelIndex - 1;
    }

    public void SetActiveLevel(int index)
    {
        activeLevelIndex = index;
    }

    public void SnapLevelToCenter(Transform level)
    {
        Vector3 pos = level.position;
        pos.x = 0;
        level.position = pos;
    }

    public void SnapNextLevelToLeft(Transform level)
    {
        Vector3 pos = level.position;
        pos.x = -14.4f;
        level.position = pos;
    }
        public void SnapNextLevelToRight(Transform level)
    {
        Vector3 pos = level.position;
        pos.x = 0;
        level.position = pos;
    }

    public void PositionLevelOffScreen(Transform level, bool toLeft)
    {
        level.position = new Vector2(toLeft ? -screenWidth : screenWidth, 0);
    }

    public void DisableLevel(int index)
    {
        Transform level = GetLevel(index);
        if (level != null)
        {
            level.gameObject.SetActive(false);
        }
    }

    public void EnableLevel(int index)
    {
        Transform level = GetLevel(index);
        if (level != null)
        {
            level.gameObject.SetActive(true);
        }
    }
}
