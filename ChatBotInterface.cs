// ChatBotInterface.cs -- NPC AI chatbot manager for Unity
// Author: Ethan Eshbaugh
// Start: 3/29/2025

//using UnityEngine;
//using Unity.Barracuda;

// --------------------------------- Class ChatBotInterface --------------------------------------
// Descript: This class serves as an interface to interact with the chatbot model.               |
// The real meat and beans of the operation going on here.                                       |
// Not sure where I want to go with it yet, so going to be updating this comment section a lot.  |
//                                                                                               |
// Functions: *instert functions here*                                                           |
//                                                                                               |
// Other Notes:                                                                                  |
//------------------------------------------------------------------------------------------------
public class ChatBotInterface : MonoBehaviour
{
    public NNModel modelAsset; //.onnx model
    private Model runtimeModel; //neural net. model instance
    private IWorker worker; // worker

    void Start()
    {
        // Setup - load modelAsset, create worker
        runtimeModel = ModelLoader.Load(modelAsset);
        worker = WorkerFacotry.CreateWorker(WorkerFactory.Type.Compute, runtimeModel);
    }

    public void ProcessInput(string input)
    {
        Tensor inputTensor = new Tensor(); // Placeholder for input tensor creation based on input string
        worker.Execute(inputTensor); // Execute the model with input tensor
        inputTensor outputTensor = worker.PeekOutput(); // Retrieve output tensor

        // ============= OUTPUT PROCESSING =============== // 
    }

    // Controller routing system to decied which model to respod with
    public string GetResponse()
    {
        if (TemplateEngnine.TryMatch(ProcessInput, out string templateResponse))
        {
            return templateResponse; // return template response
        }
        else
        {
            return ModelEngine.Generate(input); // return LM generated response
        }
    }

    // More unity integration side - look at later
    /* IEnumerator GetResponseAsync(string input) 
       {
            bool isTemplate = TemplateEngine.TryMatch(input, out string response);
            if (!isTemplate) {
                yield return StartCoroutine(ModelEngine.GenerateAsync(input));
            }

            DisplayResponse(response);
        } */
}


// TODO:
// [] class for template engine
// [] class for generative model - use barracude likely
// [] REGEX word matching system
// [] output processing
// [] more research lol
