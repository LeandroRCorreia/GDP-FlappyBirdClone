using UnityEngine;

public class PipeSpawner : MonoBehaviour, IPoolable
{
    [SerializeField] private Pipe pipeBottomPrefab;
    [SerializeField] private Pipe pipeTopPrefab;

    [SerializeField] private float minGapSize;
    [SerializeField] private float maxGapSize;

    [SerializeField] private float minGapCenter;
    [SerializeField] private float maxGapCenter;

    [SerializeField] private PassPipeTrigger passPipeTrigger;

    private Pipe bottomPipe;
    private Pipe topPipe;

    public Pipe TopPipe => topPipe;
    public Pipe BottomPipe => bottomPipe;

    public void OnInstantiated()
    {
        bottomPipe = Instantiate(pipeBottomPrefab, transform.position, Quaternion.identity, transform);
        topPipe = Instantiate(pipeTopPrefab, transform.position, Quaternion.identity, transform);

    }

    public void OnEnabledFromPool()
    {
        RandomizePipes();
        passPipeTrigger.gameObject.SetActive(true);
    }

    public void OnDisabledFromPool()
    {
        
    }

    private void RandomizePipes()
    {
        float gapPosY = transform.position.y + Random.Range(-minGapCenter, maxGapCenter);
        float gapSize = Random.Range(minGapSize, maxGapSize);

        bottomPipe.gameObject.SetActive(true);
        Vector3 bottomPipePosition = transform.position;
        bottomPipePosition.y = gapPosY - gapSize * 0.5f - (bottomPipe.Head.y - bottomPipe.transform.position.y);
        bottomPipe.transform.position = bottomPipePosition;

        topPipe.gameObject.SetActive(true);
        Vector3 topPipePosition = transform.position;
        topPipePosition.y = gapPosY + gapSize * 0.5f + (topPipe.transform.position.y - topPipe.Head.y);
        topPipe.transform.position = topPipePosition;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        DrawGap(transform.position + Vector3.up * maxGapCenter);
        DrawGap(transform.position - Vector3.up * minGapCenter);

    }

    private void DrawGap(Vector3 position)
    {
        Gizmos.DrawCube(position, Vector3.one * 0.5f);
        Gizmos.DrawLine(position - 0.5f * maxGapSize * Vector3.up, position + 0.5f * maxGapSize * Vector3.up);
    }

}
