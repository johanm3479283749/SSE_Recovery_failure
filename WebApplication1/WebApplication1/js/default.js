let eventSource = null;


function handleSSE(event) {
    console.log('handleSSE', event);
}

function submitMessage() {
    const value = $("#SSE_Message").val();

    const headerConfig = {
        'Content-Type': 'application/json'
    }
    let headers = {};

    var url = `/api/sse/message/mychannel/` + value;    

    let result = this.fetch(url, this.getSettings('POST'))
        .then(() => {
            return true;
        }).catch(() => {
            return false;
        });

    console.log('submitMessage.result', result);
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
        return Promise.resolve(false);
    }
    const headerConfig = {
        'Content-Type': 'application/json'
    }
    let headers = {};

    var channel = 'mychannel';
    var url = `/api/sse/subscribe/` + channel;
    
    let result = this.fetch(url, this.getSettings('POST'))
        .then(() => {
            console.log('message:', message);
            return true;
        }).catch(() => {
            return false;
        });

    console.log('subscribe.result', result);

    return result;
}

function handleMessage(message) {
    console.log('message received', message);
}

function initSSE() {

    console.log('initSSE');

    if (!eventSource) {
        eventSource = new EventSource('/api/event-stream?channels=mychannel&t=' + new Date().getTime());
        eventSource.addEventListener('message', this.handleServerSentEvent);
    }

    return true;
}

function handleServerSentEvent(sse) {
    console.log('handleServerSentEvent', sse);
}

function fetchData(url) {

    return fetch(url, CtdsFetch.getSettings(method, body, setHeaders));
}


