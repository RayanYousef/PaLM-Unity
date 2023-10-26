using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PaLMJSON

{
    public TextCompletion[] candidates;
    public ContentFilter[] filters;
    public SafetyFeedback[] safetyFeedback;
}


#region Palm Response
[Serializable]
public class TextCompletion
{
    public string output;
    public SafetyRating[] safetyRatings;
    public CitationSource citationMetadata;
}

[Serializable]
public class ContentFilter
{
    public string reason;
    public string message;
}

[Serializable]
public class SafetyFeedback
{
    public SafetyRating rating;
    public SafetySetting setting;
}
#endregion

#region Text Completion
[Serializable]
public class SafetyRating
{
    public string category;
    public string probability;
}

[Serializable]
public class CitationSource
{
    public string startIndex;
    public string endIndex;
    public string uri;
    public string license;
}
#endregion

#region Safety Feedback
[Serializable]
public class SafetySetting
{
    public string category;
    public string threshold;
}

#endregion




