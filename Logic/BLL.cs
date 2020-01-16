using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using SharedModels;

namespace Logic
{
    public class BLL
    {
        public List<PodcastShow> Podcasts { get; set; }
        public List<Category> Categories { get; set; }

        public RSSreader rssR { get; set; }

        public BLL()
        {
            Podcasts = new List<PodcastShow>();
            Categories = new List<Category>();
            rssR = new RSSreader();
        }

        public void PodcastAdd(string url, string category, string frequency)
        {
            PodcastShow podcast = new PodcastShow(url, category, frequency);
           addNyPodcastToList(podcast);

        }
        private void addNyPodcastToList(PodcastShow podcast)
        {
            Podcasts.Add(podcast);
        }

        public void CategoryAdd(string cat)
        {
            Category category = new Category(cat);
            Categories.Add(category);
        }

       public void serializePodcastShow()
        {
            rssR.JsonSerializer(Podcasts);
        }

       public void serializeCategory()
        {
            rssR.JsonSerializer(Categories);
        }

        public void getPodcastShos()
        {
            Podcasts = rssR.deserializePodcastlist();
        }

        public void getCategorys()
        {
            Categories = rssR.deserializeCategorylist();
        }
        public List<List<string>> ConvertPodcastListToString(string url)
        {
           
            var podcasts = new List<PodcastShow>();
            podcasts = Podcasts;
            
            var allPodcastsInString = new List<List<string>>();
            var podcastprop = new List<string>();

            foreach (PodcastShow podcast in podcasts)
            {
               
                var kategori = podcast.Kategori;
                var frekvens = podcast.Frequency;
                var name = podcast.Title;

                podcastprop.Add(name);
                podcastprop.Add(frekvens);  
                podcastprop.Add(kategori);

                allPodcastsInString.Add(podcastprop);

            }
            return allPodcastsInString;
        }
     public string läspodd(string url) {
         return rssR.läsPod(url);
        
        }
        public async Task sparapodd(string url)
        {
            await rssR.sparaPodd(url);
        }

        public int podCastEpCounter(string url)
        {
           
            var list = RSSreader.GetPodcastFeed(url);
            int counterint = 0;
            foreach (var item in list)
            {
                counterint++;
            }
            return counterint;
        }

        public List<PodcastShow> getPodcastShowByName(string name)
        {
            return Podcasts.Where(pod => pod.Title.Equals(name)).ToList();
            

        }
        public List<string> getPodcastEpisodeToString(string name)
        {
            List<string> everyEpisodeToString = new List<string>();
            var sortedPodcastListByName = getPodcastShowByName(name);

            foreach (var podcastShow in sortedPodcastListByName)
            {
                 List<Episode> everyEpisode = podcastShow.ListOfEpisodes;

                foreach (var episode in everyEpisode)
                {
                    string episodeTitle = episode.Name.ToString();
                    everyEpisodeToString.Add(episodeTitle);
                }
            }
            return everyEpisodeToString;
        }
        public string getUrlfromPodcast(string name)
        {
            var sortedList = getPodcastShowByName(name);
            var url = "";

            foreach (var pod in sortedList)
            {
                url = pod.Url;


            }

            return url;
        }
    }
   
}

