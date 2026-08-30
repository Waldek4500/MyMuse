using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyMuse.Maui.Helpers
{
    public static class SongHelper
    {
        /// <summary>
        /// Changes IsSelected and IsPlaying song's properties to correct value
        /// </summary>
        /// <param name="songs">Collection of the songs to sync</param>
        /// <param name="currentSongId">Id of the currently playing song</param>
        public static void SyncCollection(IEnumerable<Song> songs, int? currentSongId, bool isPlaying)
        {
            foreach (Song song in songs)
            {
                if(currentSongId is null)
                    song.IsSelected = song.IsPlaying = false;

                song.IsSelected = song.Data is not null && song.Data.Id == currentSongId;
                song.IsPlaying = song.Data is not null && song.Data.Id == currentSongId && isPlaying;
            }
        }
    }
}
