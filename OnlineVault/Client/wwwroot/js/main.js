

lockModal = function (truefalse) {
    $("#uploadFileModal").find("select, input, button").attr("disabled", truefalse)
}

toggleModal = function () {
    var myModal = bootstrap.Modal.getOrCreateInstance(document.getElementById('uploadFileModal'));
    myModal.toggle();
}

showErrorMessage = function (msg) {
    alert(msg);
}

togglePreviewModel = function () {
    var myModal = bootstrap.Modal.getOrCreateInstance(document.getElementById('previewModal'));
    myModal.toggle();
}

window.computeDigestUsingSubtleCrypto = async function (key, inputText) {
    // 'key' and 'inputText' are each passed as Uint8Array objects, the JS
    // equivalent of .NET's byte[]. SubtleCrypto uses ArrayBuffer objects
    // for the key and the input and output buffers.
    //
    // To convert Uint8Array -> ArrayBuffer, use the Uint8Array.buffer property.
    // To convert ArrayBuffer -> Uint8Array, call the Uint8Array constructor.
    //
    // Most of SubtleCrypto's functions - including importKey and sign - are
    // Promise-based. We await them to unwrap the Promise, similar to how we'd
    // await a Task-based function in .NET.
    //
    // https://developer.mozilla.org/docs/Web/API/SubtleCrypto/importKey

    var cryptoKey = await crypto.subtle.importKey(
        "raw" /* format */,
        key.buffer /* keyData */,
        { name: "HMAC", hash: "SHA-256" } /* algorithm (HmacImportParams) */,
        false /* extractable */,
        ["sign", "verify"] /* keyUsages */);

    // Now that we've imported the key, we can compute the HMACSHA256 digest.
    //
    // https://developer.mozilla.org/docs/Web/API/SubtleCrypto/sign

    var digest = await crypto.subtle.sign(
        "HMAC" /* algorithm */,
        cryptoKey /* key */,
        inputText.buffer /* data */);

    // 'digest' is typed as ArrayBuffer. We need to convert it back to Uint8Array
    // so that .NET properly translates it to a byte[].

    return new Uint8Array(digest);
};

window.computeDigestUsingSubtleCrypto2 = async function (key, inputText) {
    // 'key' and 'inputText' are each passed as Uint8Array objects, the JS
    // equivalent of .NET's byte[]. SubtleCrypto uses ArrayBuffer objects
    // for the key and the input and output buffers.
    //
    // To convert Uint8Array -> ArrayBuffer, use the Uint8Array.buffer property.
    // To convert ArrayBuffer -> Uint8Array, call the Uint8Array constructor.
    //
    // Most of SubtleCrypto's functions - including importKey and sign - are
    // Promise-based. We await them to unwrap the Promise, similar to how we'd
    // await a Task-based function in .NET.
    //
    // https://developer.mozilla.org/docs/Web/API/SubtleCrypto/importKey

    var cryptoKey = await crypto.subtle.importKey(
        "raw" /* format */,
        key.buffer /* keyData */,
        { name: "HMAC", hash: "SHA-256" } /* algorithm (HmacImportParams) */,
        false /* extractable */,
        ["sign", "verify"] /* keyUsages */);

    // Now that we've imported the key, we can compute the HMACSHA256 digest.
    //
    // https://developer.mozilla.org/docs/Web/API/SubtleCrypto/sign

    var digest = await crypto.subtle.sign(
        "HMAC" /* algorithm */,
        cryptoKey /* key */,
        inputText.buffer /* data */);

    // 'digest' is typed as ArrayBuffer. We need to convert it back to Uint8Array
    // so that .NET properly translates it to a byte[].

    return new Uint8Array(digest);
};

window.AESEncrypt = async function (key, iv, inputText) {
    try {
        var cryptoKey = await crypto.subtle.importKey(
            "raw" /* format */,
            BytesFromBase64(key) /* keyData */,
            "AES-CBC" /* algorithm */,
            false /* extractable */,
            ["encrypt", "decrypt"] /* keyUsages */);

        var encrypted = await window.crypto.subtle.encrypt(
            {
                name: "AES-CBC",
                iv: BytesFromBase64(iv),
            },
            cryptoKey,
            inputText.buffer,
        );
    }
    catch (e) {
        return null;
    }

    return new Uint8Array(encrypted);
}

window.AESDecrypt = async function (key, iv, cipherText) {
    try {
        var cryptoKey = await crypto.subtle.importKey(
            "raw" /* format */,
            BytesFromBase64(key) /* keyData */,
            "AES-CBC" /* algorithm */,
            false /* extractable */,
            ["encrypt", "decrypt"] /* keyUsages */);

        var decrypted = await window.crypto.subtle.decrypt(
            {
                name: "AES-CBC",
                iv: BytesFromBase64(iv),
            },
            cryptoKey,
            cipherText.buffer,
        );
    }
    catch (e) {
        return null;
    }

    return new Uint8Array(decrypted);
}

window.Base64FromBytes = function (buffer) {
    var s = '';
    var uintArray = new Uint8Array(buffer);
    uintArray.filter(function (v) { s += String.fromCharCode(v); return false; });
    return window.btoa(s);
};

window.BytesFromBase64 = function (text) {
    return new Uint8Array(
        window.atob(text)
            .split('')
            .map(function (c) {
                return c.charCodeAt(0);
            })
    );
}

window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}