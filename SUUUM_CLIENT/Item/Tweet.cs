﻿using Prism.Commands;
using Prism.Mvvm;
using SUUUM_CLIENT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUUUM_CLIENT.Item
{
    public class Tweet : BindableBase
    {
        public DelegateCommand ShowImage1 { get; set; }

        public DelegateCommand ShowImage2 { get; set; }

        public DelegateCommand ShowImage3 { get; set; }

        public DelegateCommand ShowImage4 { get; set; }

        public MainWindowViewModel ViewModel { get; set; }

        public string Text { get; set; }

        public bool HaveMedia1 { get; set; }
        public string ImageURL1 { get; set; }

        public bool HaveMedia2 { get; set; }
        public string ImageURL2 { get; set; }

        public bool HaveMedia3 { get; set; }
        public string ImageURL3 { get; set; }

        public bool HaveMedia4 { get; set; }
        public string ImageURL4 { get; set; }

        public string UserImageURL { get; set; }

        public string UserName { get; set; }

        public string UserID { get; set; }


        /// <summary>
        /// mediaが1つの場合
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="userImageURL"></param>
        /// <param name="text"></param>
        /// <param name="imageUrl"></param>
        public Tweet(MainWindowViewModel mainWindowViewModel, string id, string userName, string userImageURL, string text, string imageUrl1)
        {
            ViewModel = mainWindowViewModel;
            UserID = id;
            UserName = userName;
            UserImageURL = userImageURL;
            Text = text;
            ImageURL1 = imageUrl1;
            HaveMedia1 = true;


            ShowImage1 = new DelegateCommand(() =>
            {
            ViewModel.ShowImageURL = ImageURL1;
            },
            () => true);
        }

        /// <summary>
        /// mediaが2つの場合
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="userImageURL"></param>
        /// <param name="text"></param>
        /// <param name="imageUrl"></param>
        public Tweet(MainWindowViewModel mainWindowViewModel, string id, string userName, string userImageURL, string text, string imageUrl1, string imageUrl2)
        {
            ViewModel = mainWindowViewModel;
            UserID = id;
            UserName = userName;
            UserImageURL = userImageURL;
            Text = text;
            ImageURL1 = imageUrl1;
            HaveMedia1 = true;
            ImageURL2 = imageUrl2;
            HaveMedia2 = true;

            ShowImage1 = new DelegateCommand(() =>
            {
                ViewModel.ShowImageURL = ImageURL1;
            },
           () => true);

            ShowImage2 = new DelegateCommand(() =>
            {
                ViewModel.ShowImageURL = ImageURL2;
            },
           () => true);
        }

        /// <summary>
        /// mediaが3つの場合
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="userImageURL"></param>
        /// <param name="text"></param>
        /// <param name="imageUrl"></param>
        public Tweet(MainWindowViewModel mainWindowViewModel, string id, string userName, string userImageURL, string text, string imageUrl1, string imageUrl2, string imageUrl3)
        {
            ViewModel = mainWindowViewModel;
            UserID = id;
            UserName = userName;
            UserImageURL = userImageURL;
            Text = text;
            ImageURL1 = imageUrl1;
            HaveMedia1 = true;
            ImageURL2 = imageUrl2;
            HaveMedia2 = true;
            ImageURL3 = imageUrl3;
            HaveMedia3 = true;

            ShowImage1 = new DelegateCommand(() =>
            {
                ViewModel.ShowImageURL = ImageURL1;
            },
           () => true);

            ShowImage2 = new DelegateCommand(() =>
            {
                ViewModel.ShowImageURL = ImageURL2;
            },
           () => true);

            ShowImage3 = new DelegateCommand(() =>
            {
                ViewModel.ShowImageURL = ImageURL3;
            },
           () => true);
        }

        /// <summary>
        /// mediaが4つの場合
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="userImageURL"></param>
        /// <param name="text"></param>
        /// <param name="imageUrl"></param>
        public Tweet(MainWindowViewModel mainWindowViewModel, string id, string userName, string userImageURL, string text, string imageUrl1, string imageUrl2, string imageUrl3, string imageUrl4)
        {
            ViewModel = mainWindowViewModel;
            UserID = id;
            UserName = userName;
            UserImageURL = userImageURL;
            Text = text;
            ImageURL1 = imageUrl1;
            HaveMedia1 = true;
            ImageURL2 = imageUrl2;
            HaveMedia2 = true;
            ImageURL3 = imageUrl3;
            HaveMedia3 = true;
            ImageURL4 = imageUrl4;
            HaveMedia4 = true;

            ShowImage1 = new DelegateCommand(() =>
            {
                ViewModel.ShowImageURL = ImageURL1;
            },
           () => true);

            ShowImage2 = new DelegateCommand(() =>
            {
                ViewModel.ShowImageURL = ImageURL2;
            },
           () => true);

            ShowImage3 = new DelegateCommand(() =>
            {
                ViewModel.ShowImageURL = ImageURL3;
            },
           () => true);

            ShowImage4 = new DelegateCommand(() =>
            {
                ViewModel.ShowImageURL = ImageURL4;
            },
           () => true);
        }

        /// <summary>
        /// mediaがない場合
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="userImageURL"></param>
        /// <param name="text"></param>
        /// <param name="imageUrl"></param>
        public Tweet(MainWindowViewModel mainWindowViewModel, string id, string userName, string userImageURL, string text)
        {
            ViewModel = mainWindowViewModel;
            UserID = id;
            UserName = userName;
            UserImageURL = userImageURL;
            Text = text;
        }
    }
}