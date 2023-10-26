using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Threading.Tasks;
using System.ComponentModel;

[AddComponentMenu("PaLM Prompt Manager")]
public class PaLMPromptManager: MonoBehaviour
{

    [Header("Essential")]
    [Tooltip("PaLM REST API KEY (Register to PaLM https://developers.generativeai.google/products/palm)")]
    [SerializeField] string apiKey;
    [Tooltip("TMPro GUI that holds the output text, it is a gameObject with TextMeshProUGUI component")]
    [SerializeField] TextMeshProUGUI outputUI;

    [Header("Prompt Settings")]
    [Tooltip("A \"preprompt\" is information provided before the user's message.")]
    [TextArea(2, 5)][SerializeField] string prePrompt;
    [Tooltip("\"post prompt\" is information given after the user's message.")]
    [TextArea(2, 5)][SerializeField] string postPrompt;

    [Tooltip("The set of character sequences (up to 5) that will stop output generation. If specified, the API will stop at the first appearance of a stop sequence")]
    [SerializeField] string stopSequence;

    [Tooltip("A value closer to 1.0 will produce responses that are more varied and creative, while a value closer to 0.0 will typically result in more straightforward responses from the model.")]
    [SerializeField][Range(0, 1)] float temperature = .7f;

    [Tooltip("integer\r\n\r\nOptional. The maximum number of tokens to consider when sampling.\r\n\r\nThe model uses combined Top-k and nucleus sampling.\r\n\r\nTop-k sampling considers the set of topK most probable tokens. Defaults to 40.\r\n\r\nNote: The default value varies by model, see the Model.top_k attribute of the Model returned the getModel function.")]
    [SerializeField] int topK = 40;

    [Tooltip(" \t\r\n\r\nnumber\r\n\r\nOptional. The maximum cumulative probability of tokens to consider when sampling.\r\n\r\nThe model uses combined Top-k and nucleus sampling.\r\n\r\nTokens are sorted based on their assigned probabilities so that only the most likely tokens are considered. Top-k sampling directly limits the maximum number of tokens to consider, while Nucleus sampling limits number of tokens based on the cumulative probability.\r\n\r\nNote: The default value varies by model, see the Model.top_p attribute of the Model returned the getModel function.")]
    [SerializeField][Range(0, 1)] float topP = .95f;

    [Tooltip("Number of generated responses to return.\r\n\r\nThis value must be between [1, 8], inclusive. If unset, this will default to 1.")]
    [SerializeField] int candidateCount = 1;

    [Tooltip("The maximum number of tokens to include in a candidate.\r\n\r\nIf unset, this will default to outputTokenLimit specified in the Model specification")]
    [SerializeField] int maxOutputTokens = 256;

    [Header("SafetySettings")]
    [Tooltip("0 indicates that the threshold is unspecified, while 4 signifies that all content will be allowed")]
    [SerializeField][Range(0, 4)] int DEROGATORY = 0;

    [Tooltip("0 indicates that the threshold is unspecified, while 4 signifies that all content will be allowed")]
    [SerializeField][Range(0, 4)] int TOXICITY, VIOLENCE, SEXUAL, MEDICAL, DANGEROUS = 0;

    [Header("Prompt Events")]
    [SerializeField] UnityEvent OnRequestStarted;
    [SerializeField] UnityEvent OnRequestCompleted;
    [SerializeField] UnityEvent OnRequestFailed;

    [Header("Response Data")]
    [TextArea(5, 10)][SerializeField] string responseText;
    [SerializeField] PaLMJSON responseData;

    public async void GetResponseFromInputField(TMP_InputField userPrompt)
    {
       await GetPaLMResponse(userPrompt.text);
    }

    public async Task<PaLMJSON> GetPaLMResponse(string promptText)
    {
        OnRequestStarted.Invoke();

        promptText = prePrompt + " " + promptText + " " + postPrompt;

        string temperatureSettings = "\"temperature\":  " + temperature + " ";
        string topKSettings = " \"topK\":" + topK + "";
        string topPSettings = "\"topP\":" + topP + "";
        string candidateCountSettings = "\"candidate_count\":" + candidateCount + "";
        string maxOutputTokensSettings = "\"max_output_tokens\":" + maxOutputTokens + "";
        string stopSequenceSettings = "\"stop_sequences\":[" + stopSequence + "]";

        string safetySettings = "\"safety_settings\":[" +
            "{\"category\":\"HARM_CATEGORY_DEROGATORY\",\"threshold\":" + DEROGATORY + "}" +
            ",{\"category\":\"HARM_CATEGORY_TOXICITY\",\"threshold\":" + TOXICITY + "}," +
            "{\"category\":\"HARM_CATEGORY_VIOLENCE\",\"threshold\":" + VIOLENCE + "}," +
            "{\"category\":\"HARM_CATEGORY_SEXUAL\",\"threshold\":" + SEXUAL + "}," +
            "{\"category\":\"HARM_CATEGORY_MEDICAL\",\"threshold\":" + MEDICAL + "}," +
            "{\"category\":\"HARM_CATEGORY_DANGEROUS\",\"threshold\":" + DANGEROUS + "}]";

        string requestBody = 
            "{\"prompt\":" + "{\"text\":\"" + promptText + "\"}," +
            "" + temperatureSettings + "," +
            "" + topKSettings + "," +
            "" + topPSettings + "," +
            "" + candidateCountSettings + "," +
            "" + maxOutputTokensSettings + "," +
            "" + stopSequenceSettings + "," +
            "" + safetySettings + "}";

        responseData = await PaLM.GetPaLMResponse(requestBody, apiKey, OnRequestCompleted.Invoke, OnRequestFailed.Invoke);

        if(responseData.candidates!=null)
        responseText = responseData.candidates[0].output;

        UpdateOutputUIText(responseText);

        return responseData;
    }

    public void UpdateOutputUIText(string text)
    {
        if (outputUI == null)
            return;

        outputUI.text = text;
    }

}
