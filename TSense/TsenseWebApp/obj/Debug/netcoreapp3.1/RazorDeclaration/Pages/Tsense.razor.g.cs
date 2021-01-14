// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace TsenseWebApp.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Facultate\Anul III Semestrul I\.NET\TSense\.NetProject\TSense\TsenseWebApp\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Facultate\Anul III Semestrul I\.NET\TSense\.NetProject\TSense\TsenseWebApp\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Facultate\Anul III Semestrul I\.NET\TSense\.NetProject\TSense\TsenseWebApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Facultate\Anul III Semestrul I\.NET\TSense\.NetProject\TSense\TsenseWebApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Facultate\Anul III Semestrul I\.NET\TSense\.NetProject\TSense\TsenseWebApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Facultate\Anul III Semestrul I\.NET\TSense\.NetProject\TSense\TsenseWebApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Facultate\Anul III Semestrul I\.NET\TSense\.NetProject\TSense\TsenseWebApp\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Facultate\Anul III Semestrul I\.NET\TSense\.NetProject\TSense\TsenseWebApp\_Imports.razor"
using TsenseWebApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Facultate\Anul III Semestrul I\.NET\TSense\.NetProject\TSense\TsenseWebApp\_Imports.razor"
using TsenseWebApp.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Facultate\Anul III Semestrul I\.NET\TSense\.NetProject\TSense\TsenseWebApp\_Imports.razor"
using MatBlazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Facultate\Anul III Semestrul I\.NET\TSense\.NetProject\TSense\TsenseWebApp\Pages\Tsense.razor"
using TsenseWebApp.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Facultate\Anul III Semestrul I\.NET\TSense\.NetProject\TSense\TsenseWebApp\Pages\Tsense.razor"
using Newtonsoft.Json.Linq;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Facultate\Anul III Semestrul I\.NET\TSense\.NetProject\TSense\TsenseWebApp\Pages\Tsense.razor"
using TsenseWebApp.Config;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Tsense : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 144 "D:\Facultate\Anul III Semestrul I\.NET\TSense\.NetProject\TSense\TsenseWebApp\Pages\Tsense.razor"
       

    Tweet tweet = new Tweet();
    string user = "";
    string result = "";
    string text2 = "";
    bool sentiment1 =true;
    bool sentiment2 = true;
    bool sentiment3 = true;

    Boolean ShowImage1 = true;
    Boolean ShowImage2 = true;
    Boolean ShowImage3 = true;
    double probability1 = 0;
    double probability2 = 0;
    double probability3 = 0;


    protected async Task OnSubmitLink()
    {
        result = await TService.GetTextFromTweet(tweet.Link);
        if (result != "Wrong link")
        {
            JObject mlPrediction = await MService.SentimentFromLink(result);
            sentiment1 = (bool)(mlPrediction)[Constants.Prediction];
            probability1 = Math.Round((double)(mlPrediction)[Constants.Probability], Constants.NumberOfDecimals);

        }
        else
        {
            //de tratat caz in care nu exista username
        }
        ShowImage1 = false;


    }


    protected async Task OnSubmitUser()
    {


        List<string> tweets = await TService.GetTweetsFromUser(user);
        if (tweets.Contains("Wrong username"))
        {
            JObject mlPrediction = await MService.SentimentFromMultiple(tweets);
            sentiment3 = (bool)(mlPrediction)[Constants.Prediction];
            probability3 = Math.Round((double)(mlPrediction)[Constants.Probability], Constants.NumberOfDecimals);

        }
        else
        {
            //tratat caz in care nu exista username
        }
        ShowImage3 = false;


    }
    protected async Task OnSubmitText()
    {

        JObject mlPrediction = await MService.SentimentFromText(text2);
        sentiment2 = (bool)(mlPrediction)[Constants.Prediction];
        probability2 = Math.Round((double)(mlPrediction)[Constants.Probability], Constants.NumberOfDecimals);
        ShowImage2 = false;
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private MLService MService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private TweetService TService { get; set; }
    }
}
#pragma warning restore 1591
