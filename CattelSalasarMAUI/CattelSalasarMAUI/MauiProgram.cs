using CattelSalasarMAUI.Services.IService;
using CattelSalasarMAUI.Services.Service;
using CattelSalasarMAUI.SQLiteDB;
using CattelSalasarMAUI.ViewModels;
using CattelSalasarMAUI.Views;
using CattelSalasarMAUI.Views.ClaimIntimation;
using CattelSalasarMAUI.Views.ProposalDetails;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace CattelSalasarMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            //Services register
            builder.Services.AddTransient<IAddressService, AddressService>();
            builder.Services.AddTransient<ILoginPageService, LoginPageService>();
            builder.Services.AddTransient<IBaseDataService, BaseDataService>();
            builder.Services.AddTransient<ICreateProposalService, CreateProposalService>();
            builder.Services.AddTransient<IEditProposalService, EditProposalService>();
            builder.Services.AddTransient<IUploadProposalService, UploadProposalService>();
            builder.Services.AddTransient<IClaimIntimationService, ClaimIntimationService>();

            //views_&_ViewModel register
            builder.Services.AddTransient<LoginPage, LoginPageViewModel>();
            builder.Services.AddTransient<RegistrationPage, UserRegistrationViewMode>();
            builder.Services.AddTransient<HomePage, HomePageViewModel>();
            builder.Services.AddTransient<CreateProposal, ProposalDetailsViewModel>();
           // builder.Services.AddTransient<EditPreviewPage, ProposalDetailsViewModel>();
            builder.Services.AddTransient<UploadPreviewPage, UploadProposalViewModel>();
            builder.Services.AddTransient<ProposalAnimalDetails, ProposalAnimalDetailsViewModel>();
            builder.Services.AddTransient<UploadAnimalDetailsPage, ProposalAnimalDetailsViewModel>();
            builder.Services.AddTransient<UploadAnimalCardPage, ProposalAnimalDetailsViewModel>();
            builder.Services.AddTransient<EditPreviewPage, EditProposalViewModel>();
            builder.Services.AddTransient<EditProposalDetails, ProposalDetailsViewModel>(); 
            builder.Services.AddTransient<EditAnimalCardPage, ProposalAnimalDetailsViewModel>(); 
            builder.Services.AddTransient<EditAnimalDetailsPage, ProposalAnimalDetailsViewModel>();
            builder.Services.AddTransient<CardClaimIntimation, CardClaimIntimationViewModel>();
            builder.Services.AddTransient<ClaimIntimationPage, ClaimIntimationViewModel>(); 
            builder.Services.AddTransient<ClaimAnimalCardPage, ClaimAnimalViewModel>(); 
            builder.Services.AddTransient<ClaimIntimationImagePage, ClaimAnimalViewModel>(); 
            builder.Services.AddTransient<UploadClaimIntimationCard, UploadClaimIntimationViewModel>(); 
            builder.Services.AddTransient<UploadClaimIntimation, UploadClaimIntimationViewModel>(); 
            
            //Testing data
            builder.Services.AddTransient<TestingPage, ProposalAnimalDetailsViewModel>(); 
            
           // builder.Services.AddTransient<EditAnimalCardPage, EditAnimalCardPageViewModel>(); 


            //DataBase register
            builder.Services.AddSingleton<AnimalDetailDB>();
            builder.Services.AddSingleton<ProposalImageDB>();
            builder.Services.AddSingleton<ClaimImageDB>();
            builder.Services.AddSingleton<ClaimIntimationBasicDetailsDB>();

            //builder.Services.AddT

            return builder.Build();
        }
    }
}
