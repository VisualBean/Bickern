using Bickern.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System;

namespace Bickern.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<LocalSite> activeSites;

        private IHostFileService hostFileService;
        private IIPService ipService;
        private bool isServing;
        private ILocalSiteFactory localSiteFactory;
        private IProxyService proxyService;
        private ISiteArchive siteArchive;

        public MainViewModel(IHostFileService hostFileService, IProxyService proxyService, IIPService ipService, ILocalSiteFactory localSiteFactory, ISiteArchive siteArchive)
        {
            this.hostFileService = hostFileService;
            this.proxyService = proxyService;
            this.ipService = ipService;
            this.localSiteFactory = localSiteFactory;
            this.siteArchive = siteArchive;
            IsServing = true;

            var sites = ReadSitesConfig();
            if (sites == null)
                ActiveSites = new ObservableCollection<LocalSite>();
            else
                ActiveSites = new ObservableCollection<LocalSite>(sites);
            OpenUrlCommand = new RelayCommand<string>(OpenUrl);
            RemoveSiteCommand = new RelayCommand<LocalSite>(RemoveSite);
            QuitCommand = new RelayCommand(Quit);
            AddSiteCommand = new RelayCommand(AddSite);
            
        }

        private void Quit()
        {
            Application.Current.Shutdown();
        }

        public ObservableCollection<LocalSite> ActiveSites { get => activeSites; set { activeSites = value; RaisePropertyChanged("ActiveSites"); ActiveSitesChanged(); } }

        public RelayCommand AddSiteCommand { get; set; }
        public RelayCommand QuitCommand { get; set; }
        public bool IsServing { get => isServing; set { isServing = value; RaisePropertyChanged(); ChangedServeState(isServing); } }

        public RelayCommand<string> OpenUrlCommand { get; set; }

        public RelayCommand<LocalSite> RemoveSiteCommand { get; set; }


        private void ActiveSitesChanged()
        {

            siteArchive.ArchiveSites(ActiveSites);
        }

        private void AddSite()
        {
            var view = new AddSiteDialog();
            if (view.ShowDialog() != true)
            {
                return;
            }

            var path = view.Path;
            var url = view.Url;
            DirectoryInfo dir;
            if (!Directory.Exists(path))
            {
                MessageBox.Show("That wasn't a real directory was it? - Please try adding it again.");
                AddSite();
            }
            else
            {
                dir = new DirectoryInfo(path);
                LocalSite site= null;
                try
                {
                    site = localSiteFactory.CreateLocalSite(dir, url);
                }
                catch 
                {
                    MessageBox.Show("Something went wrong, when trying to create the site. Please try again.");
                    return;
                   
                }
                if (site == null)
                {
                    MessageBox.Show("Folder does not contain an index file.");
                    return;
                }
                ActiveSites.Add(site);
            }
        }

        private void ChangedServeState(bool isServing)
        {
            if (!isServing)
            {
                foreach (var site in ActiveSites)
                    site.Stop();
            }
            else
            {
                var sites = ReadSitesConfig();
                if(sites != null)
                ActiveSites = new ObservableCollection<LocalSite>(sites);
                
            }
        }

        private void OpenUrl(string url)
        {
            Process.Start($"http://{url}");
        }

        private IEnumerable<LocalSite> ReadSitesConfig()
        {
            var sites = siteArchive.GetArchivedSites();
            if (sites == null)
                return null;
            var localSites = new List<LocalSite>();
            foreach (var site in sites)
            {
                var dir = new DirectoryInfo(site.path);
                var localSite = localSiteFactory.CreateLocalSiteFromIP(dir, site.url, site.ip);
                if (localSite != null)
                    localSites.Add(localSite);
            }
            return localSites;
        }

        private void RemoveSite(LocalSite site)
        {
            site.Dispose();
            ActiveSites.Remove(site);
        }
    }
}