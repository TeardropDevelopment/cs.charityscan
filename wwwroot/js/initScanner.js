var resultContainer;
var lastResult, countResults = 0;

var html5QrCode;

function initScanner(callBack) {
    html5QrCode = new Html5Qrcode("qr-reader");

    const qrCodeSuccessCallback = (decodedText, decodedResult) => {

        callBack(decodedText);

        html5QrCode.pause();
        console.log(`Scan result ${decodedText}`, decodedResult);
    }
    const config = {
        fps: 10, qrbox: { height: 250, width: 250 }
    };

    // If you want to prefer back camera
    html5QrCode.start({ facingMode: "environment" }, config, qrCodeSuccessCallback);
}

// Proxy function
// blazorInstance: A reference to the actual C# class instance, required to invoke C# methods inside it
// blazorCallbackName: parameter that will get the name of the C# method used as callback
window.scannerProxy = (instance, callbackMethod) => {
    // Execute function that will do the actual job
    window.initScanner(result => {
        // Invoke the C# callback method passing the result as parameter
        instance.invokeMethodAsync(callbackMethod, result);
    });
}