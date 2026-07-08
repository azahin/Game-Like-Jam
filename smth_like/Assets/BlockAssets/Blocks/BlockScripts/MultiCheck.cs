using UnityEngine;

public class MultiCheck : MonoBehaviour
{
    [SerializeField] private byte checkType; //Check if block is 1, 2, 3... to make factories happen

    public bool Collide;
    private void OnCollisionEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Container>().GetComponent<Voxel>().Id == checkType)
        {
            Collide = true;
        }

       
    }
}
