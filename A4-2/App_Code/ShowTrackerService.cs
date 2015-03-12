using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ShowTrackerService" in code, svc and config file together.
public class ShowTrackerService : IShowTrackerService
{
    ShowTrackerEntities ste = new ShowTrackerEntities();
    public bool AddArtist(Artist a)
    {
        bool result = true;
        try
        {
            Artist art = new Artist();
            art.ArtistName = a.ArtistName;
            art.ArtistEmail = a.ArtistEmail;
            art.ArtistWebPage = a.ArtistWebPage;
            art.ArtistDateEntered = DateTime.Now;
            ste.Artists.Add(art);
            ste.SaveChanges();
        }
        catch
        {
            result = false;
        }
        return result;
    }

    public bool AddShow(Show s, ShowDetail sd)
    {
        bool result = true;
        try
        {
            Show sh = new Show();
            sh.ShowName = s.ShowName;
            sh.ShowDate = s.ShowDate;
            sh.ShowTime = s.ShowTime;
            sh.ShowTicketInfo = s.ShowTicketInfo;
            sh.ShowDateEntered = DateTime.Now;
            ShowDetail shdet = new ShowDetail();
            shdet.ShowDetailArtistStartTime = sd.ShowDetailArtistStartTime;
            shdet.ShowDetailAdditional = sd.ShowDetailAdditional;
            ste.Shows.Add(sh);
            ste.ShowDetails.Add(shdet);
            ste.SaveChanges();
        }
        catch
        {
            result = false;
        }
        return result;
    }

    public List<Artist> GetArtist()
    {
        var artists = from a in ste.Artists
                      orderby a.ArtistName
                      select new { a.ArtistName, a.ArtistKey };

        List<Artist> artistsObj = new List<Artist>();
        foreach (var a in artists)
        {
            Artist art = new Artist();
            art.ArtistName = a.ArtistName;
            art.ArtistKey = a.ArtistKey;
            artistsObj.Add(art);
        }
        return artistsObj;
    }
}
