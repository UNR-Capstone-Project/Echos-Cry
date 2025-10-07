using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//TODO: Determine how many notes player needs to consecutively hit to perform attack.
//TODO: Adjust engine timescale outside of the scope of this function.

public class MinigameManager : MonoBehaviour
{
    public GameObject notePrefab;
    public GameObject parentMask;
    private TextMeshProUGUI trackTimeComponent;

    public const float timeWindow = 8f; //How many seconds are centering the surrounding the current track time of the song that are previewed within the minigame. Essentially your "view" of the current song.
    public const int MAX_HIT_TRIES = 5;
    private static float reactionTime = timeWindow / 2; //Time in seconds that a note will spawn before reaching its onset time.

    private const float tempo = 85f; //Tempo is measured in beats per minute
    private static float beat = 60f / tempo;

    private float currentTrackTime = -reactionTime; //Start negative in case a note is played at time 0 seconds in the song!
    private int noteCount = 0;
    private int successfulNoteHits = 0;
    private int failedNoteHits = 0;
    private bool isDestroyed = false;

    private note[] song;

    private enum NOTE_PITCH
    {
        A = 0,
        ASharp = 1,
        BFlat = 1,
        B = 2,
        C = 3,
        CSharp = 4,
        DFlat = 4,
        D = 5,
        DSharp = 6,
        EFlat = 6,
        E = 7,
        F = 8,
        FSharp = 9,
        GFlat = 9,
        G = 10,
        GSharp = 11,
        AFlat = 11
    }

    private enum NOTE_NAME
    {
        WholeNote,
        HalfNote,
        QuarterNote,
        EighthNote
    }

    private static Dictionary<NOTE_NAME, float> noteDuration = new Dictionary<NOTE_NAME, float>
    { //How many beats a note is.
        {NOTE_NAME.WholeNote, 4.0f },
        {NOTE_NAME.HalfNote, 2.0f },
        {NOTE_NAME.QuarterNote, 1.0f },
        {NOTE_NAME.EighthNote, 0.5f }
    };

    private struct note
    {
        public NOTE_PITCH pitch;
        public float duration; //Note duration in seconds
        public float onset;

        public note(NOTE_NAME name, NOTE_PITCH pitch, float onset)
        {

            this.duration = beat * noteDuration[name];
            this.pitch = pitch;
            this.onset = onset;
        }
    }

    void Start()
    {
        Canvas mCanvas = GetComponent<Canvas>();
        mCanvas.worldCamera = Camera.main;
        mCanvas.planeDistance = 0;

        trackTimeComponent = GetComponentInChildren<TextMeshProUGUI>();

        song = new note[5]; //A song with 3 total notes.
        song[0] = new note(NOTE_NAME.QuarterNote, NOTE_PITCH.A, 0f);
        song[1] = new note(NOTE_NAME.QuarterNote, NOTE_PITCH.G, 1f);
        song[2] = new note(NOTE_NAME.QuarterNote, NOTE_PITCH.AFlat, 2f);
        song[3] = new note(NOTE_NAME.QuarterNote, NOTE_PITCH.BFlat, 3f);
        song[4] = new note(NOTE_NAME.QuarterNote, NOTE_PITCH.D, 4f);
    }

    IEnumerator TimedDestroy()
    {
        yield return new WaitForSeconds(1);
        //GameObject.FindWithTag("Player").GetComponent<Player>().closeMiniGame();
        Destroy(gameObject);
    }

    void Update()
    {
        if (failedNoteHits + successfulNoteHits >= MAX_HIT_TRIES && !isDestroyed)
        {
            StartCoroutine(TimedDestroy());
            isDestroyed = true;
        }

        currentTrackTime += Time.deltaTime;
        //ISSUE: To sync with audio use audioSource.time; instead!

        if (currentTrackTime >= 0)
        {
            int minutes = Mathf.FloorToInt(currentTrackTime / 60);
            int seconds = Mathf.FloorToInt(currentTrackTime % 60);

            trackTimeComponent.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        int noteCachedAmount = 4; //How many notes can be spawned simultaneously.
        int maxCachedAmount = Mathf.Min(noteCount + noteCachedAmount, song.Length);

        for (int i = noteCount; i < maxCachedAmount; i++)
        { //Cache reduces looping for songs with many notes.
            float spawnNoteTime = song[i].onset - reactionTime;
            float tolerance = 0.1f;

            if (currentTrackTime >= spawnNoteTime && currentTrackTime < spawnNoteTime + tolerance)
            {
                SpawnNote(song[i]);
                noteCount++;
            }
        }
    } 
    //(measure # - 1 * how many beats per measure) + what beat your in) * (tempo/60) = onset
    //tempo / 60 seconds -> gives rythm

    void SpawnNote(note currentNote)
    {
        //Spawn note here!
        //Note must positionally reach the end of the visible window in "timeWindow" seconds.
        //Basically Lerp the notes position by a linear factor to the end.
        //Spawn position.y would be controlled by the pitch!
        Debug.Log("Note has been spawned!");

        GameObject spawnedNote = Instantiate(notePrefab, parentMask.transform);

        float maskWidth = parentMask.GetComponent<RectTransform>().rect.width * 2;
        Vector3 startNotePos = new Vector3(-maskWidth / 2, 0f, 0f);
        Vector3 endNotePos = new Vector3(maskWidth / 2, 0f, 0f);

        spawnedNote.transform.localPosition = startNotePos;
        spawnedNote.GetComponent<MusicNote>().setupNote(currentNote.pitch.ToString(), currentNote.duration, maskWidth / timeWindow); //maskWidth / timeWindow gives the relative size of one second
        spawnedNote.GetComponent<MusicNote>().setDestroyTimer(timeWindow);

        StartCoroutine(LerpNotePosition(spawnedNote, startNotePos, endNotePos));
    }

    IEnumerator LerpNotePosition(GameObject noteObj, Vector3 startPos, Vector3 endPos)
    {
        float elapsed = 0f;

        while (elapsed < timeWindow)
        {
            if (noteObj == null) { yield break; }
            noteObj.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsed / timeWindow);
            elapsed += Time.deltaTime;
            yield return null;
        }

        noteObj.transform.localPosition = endPos;
    }

    public void addSuccessfulNoteHit()
    {
        successfulNoteHits++;
        Debug.Log(successfulNoteHits.ToString());
    }
    public void addFailedNoteHit()
    {
        failedNoteHits++;
        Debug.Log(failedNoteHits.ToString());
    }

}
