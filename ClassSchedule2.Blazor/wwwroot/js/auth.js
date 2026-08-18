window.authLogin = async function (url, payload) {
    const res = await fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
        credentials: 'include'
    });

    const text = await res.text();
    return { ok: res.ok, status: res.status, text };
};

window.authGet = async function (url) {
    const res = await fetch(url, {
        method: 'GET',
        credentials: 'include'
    });

    const text = await res.text();
    return { ok: res.ok, status: res.status, text };
};

window.authLogout = async function (url) {
    const res = await fetch(url, {
        method: 'POST',
        credentials: 'include'
    });

    const text = await res.text();

    return {
        ok: res.ok,
        status: res.status,
        text: text
    };
};

window.authPost = async function (url, payload) {
    const res = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(payload),
        credentials: 'include'
    });

    const text = await res.text();

    return {
        ok: res.ok,
        status: res.status,
        text: text
    };
};

window.authDelete = async function (url, payload) {
    const res = await fetch(url, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(payload),
        credentials: 'include'
    });

    const text = await res.text();

    return {
        ok: res.ok,
        status: res.status,
        text: text
    };
};

window.authDeleteNoBody = async function (url) {
    const res = await fetch(url, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        },
        credentials: 'include'
    });

    const text = await res.text();

    return {
        ok: res.ok,
        status: res.status,
        text: text
    };
};

window.authPatch = async function (url, payload) {
    const res = await fetch(url, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(payload),
        credentials: 'include'
    });

    const text = await res.text();

    return {
        ok: res.ok,
        status: res.status,
        text: text
    };
};