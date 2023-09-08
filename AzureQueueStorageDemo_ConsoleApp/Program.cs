// See https://aka.ms/new-console-template for more information
using Azure.Storage.Queues.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

Console.WriteLine("Azure Storage Queue Demo by Amrut");

// Retrieve storage account from connection string.(Copy connection string from Access Keys of Storage Account)
CloudStorageAccount storageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=amrutstorageaccount;AccountKey=Z826mgzIPpBlYogWn8aDpHkiVFxs9T8xufvzoJp62w903DbIU1v0Ho8TzzY+LJI7co9Rk9tgTSLg+AStpxZilQ==;EndpointSuffix=core.windows.net");

// Create the queue client.
CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

// Retrieve a reference to a container.
CloudQueue queue = queueClient.GetQueueReference("myqueues");

// Create the queue if it doesn't already exist
//await queue.CreateIfNotExistsAsync();

#region Add Message to Queue
//Console.WriteLine("\nAdding messages to the queue...");

///// Create a message and add it to the queue.
//CloudQueueMessage message = new CloudQueueMessage("First Message from C#");
//await queue.AddMessageAsync(message);

//message = new CloudQueueMessage("Second Message from C#");
//await queue.AddMessageAsync(message);

#endregion
#region Read Message from Queue: Without Processing Data (Peek : doesn't alter the visibility of the message)
//Always retrieve current message
Console.WriteLine("\nPeek at the messages in the queue...");

var peekedMessage = await queue.PeekMessageAsync();
Console.WriteLine($"Message: {peekedMessage.AsString}");

//// Peek at messages in the queue
//var peekedMessages = await queue.PeekMessagesAsync(10);

//foreach (var peekedMessage in peekedMessages)
//{
//    // Display the message
//    Console.WriteLine($"Message: {peekedMessage.AsString}");
//}
#endregion

#region Read Message from Queue: With Processing Data (DQueue: Invisible for 30 seconds default)
//Lock-Read-Process-Delete (Every message is processed ONLY ONCE  and by ONLY ONE client

Console.WriteLine("\nGet the messages from the queue...");
var msg = await queue.GetMessageAsync();
Console.WriteLine($"Message: {msg.AsString}");
//await queue.DeleteMessageAsync(msg);
#endregion

#region Read Message from Queue: With Processing Data (DQueue: Invisible for 30 seconds default)
//Lock-Read-Process-Delete (Every message is processed ONLY ONCE  and by ONLY ONE client

Console.WriteLine("\nGet the messages from the queue...");
var updateMessage = await queue.GetMessageAsync();
updateMessage.SetMessageContent("this is changed");
Console.WriteLine($"Message updated: {updateMessage.AsString}");

//await queue.UpdateMessageAsync(updateMessage.AsString);// (updateMessage, new TimeSpan(0,1,0));
#endregion
