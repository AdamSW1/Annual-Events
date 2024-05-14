using System.Collections.Generic;
using BusinessLayer;
using DataLayer;

namespace MalfunctioningKitchen.ViewModels;

public class WelcomeViewModel : ViewModelBase
{
    public List<Annual_Events_User> Users {get;}

    public WelcomeViewModel()
    {
        Users = new(AnnualEventsService.Instance.DbContext.Annual_Events_User);
    }
}