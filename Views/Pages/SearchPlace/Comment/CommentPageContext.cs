using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using IoC_Container.Attributes;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Utilties.Navigation;
using static GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response.PlaceDetailResponse;

namespace TravelPlanning.Views.Pages.SearchPlace.Comment
{
    [Transient]
    [AddINotifyPropertyChangedInterface]
    public class CommentPageContext : INavigationAware
    {

        public ObservableCollection<Review> Reviews { get; set; }

        public void SendAware(object parm)
        {
            var datas = (Review[])parm;
            Reviews =  new ObservableCollection<Review>(datas);
        }
    }
}
