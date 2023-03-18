﻿@ModelType List(Of RPGCor.MMM.Core.Model.BoxOffice)

@Code
    Layout = Nothing
End Code

<span style="font-family:MovieTitle; font-size:26px;">Top Box Office Movies</span><br />
<table border="1" cellpadding="5" cellspacing="0" style="width:95%; margin:10px auto; background-color:#CCC;">
<tr style="font-family:MovieTitle; font-size:20px;">
    <td>#</td>
    <td>Movie</td>
    <td>Rating</td>
    <td>RunTime</td>
    <td>Released</td>
</tr>
@For i As Integer = 1 To Model.Count
    @<tr style="font-size:16px;">
        <td width="50px">@(i)</td>
        <td style="padding:2px;"><a href="@(Model(i-1).Url)" target="_blank">@(model(i-1).Title)</a><br />Staring: @(string.Join(", ", Model(i-1).Cast.Take(1).ToArray))</td>
        <td width="90px">@(model(i-1).Rating)</td>
        <td width="100px">@(Model(i-1).RunTime)</td>
        <td width="100px">@(model(i-1).Released.ToShortDateString)</td>
    </tr>
Next
</table>