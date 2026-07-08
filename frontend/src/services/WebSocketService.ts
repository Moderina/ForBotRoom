import * as signalR from "@microsoft/signalr";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5132/chatHub")
    .withAutomaticReconnect()
    .build();

export async function startSignalR(userId : string) {
    await connection.start();
    await connection.invoke("RegisterUser", userId);
    console.log("SignalR connected");
}

export function onReceiveMessage(
    callback: (message: any) => void
) {
    connection.on("ReceiveMessage", callback);
}