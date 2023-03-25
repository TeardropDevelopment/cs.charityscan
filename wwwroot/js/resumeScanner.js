async function resumeScanner() {
    await new Promise(r => setTimeout(r, 2000));

    html5QrCode.resume();
}