public class IndexFunc
{
    // インデックスを範囲内で循環させる（増減する）
    public int GetCyclicIndex(int currentIndex, int direction, int length)
    {
        // インデックスを増減
        int nextIndex = currentIndex + direction;

        // 範囲内に収める処理
        if (nextIndex < 0)
        {
            nextIndex = length - 1; // 範囲外になったら最大インデックスに戻す
        }
        else if (nextIndex >= length)
        {
            nextIndex = 0; // 範囲外になったら最小インデックスに戻す
        }

        return nextIndex;
    }
}
