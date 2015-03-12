using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VenueRegService" in code, svc and config file together.
public class VenueRegService : IVenueRegService
{
    ShowTrackerEntities1  ste = new ShowTrackerEntities1();
    public bool RegisterVenue(Venue v, VenueLogin vl)
    {
        bool result = true;
        try
        {
            PasswordHash ph = new PasswordHash();
            KeyCode kc = new KeyCode();
            int code = kc.GetKeyCode();
            byte[] hashed = ph.HashIt(vl.VenueLoginPasswordPlain, code.ToString());

            Venue ven = new Venue();
            ven.VenueName = v.VenueName;
            ven.VenueAddress = v.VenueAddress;
            ven.VenueCity = v.VenueCity;
            ven.VenueState = v.VenueState;
            ven.VenueZipCode = v.VenueZipCode;
            ven.VenuePhone = v.VenuePhone;
            ven.VenueEmail = v.VenueEmail;
            ven.VenueWebPage = v.VenueWebPage;
            ven.VenueAgeRestriction = v.VenueAgeRestriction;
            ven.VenueDateAdded = DateTime.Now;
            VenueLogin venlog = new VenueLogin();
            venlog.VenueLoginUserName = vl.VenueLoginUserName;
            venlog.VenueLoginPasswordPlain = vl.VenueLoginPasswordPlain;
            venlog.VenueLoginRandom = code;
            venlog.VenueLoginHashed = hashed;
            venlog.VenueLoginDateAdded = DateTime.Now;
            ste.Venues.Add(ven);
            ste.VenueLogins.Add(venlog);
            ste.SaveChanges();
        }
        catch
        {
            result = false;
        }
        return result;
    }

    public int VenueLogin(string userName, string Password)
    {
        int id = 0;

        LoginClass lc = new LoginClass(Password, userName);
        id = lc.ValidateLogin();

        return id;
    }
}