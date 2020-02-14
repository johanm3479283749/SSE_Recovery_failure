let eventSource = null;

function submitMessage() {
    const value = $('#SSE_Message').val();

    const headerConfig = {
        'Content-Type': 'application/json'
    }
    let headers = {};

    var url = `/api/sse/message/` + value;    

    let result = this.fetch(url, this.getSettings('POST'));
}

function addMessage(message) {
    if (typeof message != 'string') {
        message = JSON.stringify(message);
    }

    console.log('message', message);

    var table = document.getElementById("messageTable");

    var row = table.insertRow(0);

    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);

    cell1.innerHTML = new Date();
    cell2.innerHTML = message;
}

function getSettings (method, body) {
    const settings = {
        method,
        headers: new Headers({
            'Content-Type': 'application/json'
            }
        )
    };

    if (method === 'PUT' || method === 'POST') {
        settings.body = body;
    }

    return settings;
}

function subscribe() {
    if (!this.initSSE()) {
        return;
    }

    const headerConfig = {
        'Content-Type': 'application/json'
    }
    let headers = {};

    var channel = 'mychannel';
    var url = `/api/sse/subscribe/` + channel;
    
    let result = this.fetch(url, this.getSettings('POST'))
        .catch(response => {
            addMessage("ERROR: subscribe");
            addMessage("ERROR: " + response);
        });

    return result;
}

function initSSE() {

    if (!eventSource) {
        try {
            eventSource = new EventSource('/api/event-stream?channels=mychannel&t=' + new Date().getTime());
            $(eventSource).handleServerEvents({
                handlers: {
                    onConnect: function(e) {
                        console.log('onConnect',e);
                    },
                    onMessage: function (e) {
                        console.log('onMessage',e);
                        addMessage(e);
                    }
                }
            });            
        }
        catch(ex)
        {
            addMessage("ERROR: initSSE");
            addMessage("ERROR: " + ex);
        }
    }

    return true;
}

function handleServerSentEvent(sse) {
    addMessage(sse.data);
}

function fetchData(url) {

    return fetch(url, CtdsFetch.getSettings(method, body, setHeaders));
}


