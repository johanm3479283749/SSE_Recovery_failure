
function submitMessage() {
    const value = $("#SSE_Message").val();
    console.log('submitMessage', value);
}

function subscribe() {
    if (!this.initSSE()) {
        return Promise.resolve(false);
    }
    var url = `/api/subscribe/{SSE_Message_Test}`;

    return this.$_ctdsUtilities.fetchData(url, 'POST')
        .then(() => {
            return true;
        }).catch(() => {
            return false;
        });
}

function parseEvent(sse) {
    var result = /([^/]+)\/([^@]+)@([^\s]+)\s(.+)/.exec(sse.data);

    if (result && result.length > 4) {
        var payload = result[4];
        return JSON.parse(payload);
    }
    return null;
}

function initSSE() {

    if (this.eventSource === null) {
        this.eventSource = new EventSource('/api/event-stream');
        this.eventSource.addEventListener('message', this.handleServerSentEvent);
    }

    return true;
}

function handleServerSentEvent(sse) {
    var event = this.parseEvent(sse);
    if (event !== null) {
        this.$emit(event.channel, event);
    }
}

function fetchData(url) {

    return fetch(url, CtdsFetch.getSettings(method, body, setHeaders));
}


