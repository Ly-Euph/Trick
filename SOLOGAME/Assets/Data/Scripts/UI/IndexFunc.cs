public class IndexFunc
{
    // �C���f�b�N�X��͈͓��ŏz������i��������j
    public int GetCyclicIndex(int currentIndex, int direction, int length)
    {
        // �C���f�b�N�X�𑝌�
        int nextIndex = currentIndex + direction;

        // �͈͓��Ɏ��߂鏈��
        if (nextIndex < 0)
        {
            nextIndex = length - 1; // �͈͊O�ɂȂ�����ő�C���f�b�N�X�ɖ߂�
        }
        else if (nextIndex >= length)
        {
            nextIndex = 0; // �͈͊O�ɂȂ�����ŏ��C���f�b�N�X�ɖ߂�
        }

        return nextIndex;
    }
}
