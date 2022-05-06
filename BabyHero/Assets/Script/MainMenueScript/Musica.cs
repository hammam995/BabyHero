using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Musica : MonoBehaviour
{
    // to controll to the slider to controll the music volum and soun
    public AudioSource musica;
    public List<AudioClip> musicaList = new List<AudioClip>();
    private int itemspot = 0;
    private float MusicVolum = 1;
    private void Start()
    {
        musica = GetComponent<AudioSource>();
    }
    public void ForwardMusica()
    {
        if ( Input.GetKeyDown("9")    && itemspot < musicaList.Count - 1)
        {
           musica.clip  = musicaList[itemspot];
            itemspot++;
            musica.Play(0);
        }
       else if (Input.GetKeyDown("9") && itemspot == musicaList.Count - 1)
        {
            musica.clip = musicaList[itemspot];
            itemspot = 0;
            musica.Play(0);
        }
    }
    public void BackwardMusica()
    {
        if (Input.GetKeyDown("8") && itemspot > 0)
        {
            itemspot--;
            musica.clip = musicaList[itemspot];
            musica.Play(0);
        }
    }
    public void setVolum(float vol)
    {
        MusicVolum = vol;
    }
    void Update()
    {
        ForwardMusica();
        BackwardMusica();
    }

}
