export interface ValidationMessages {
    [key: string]: (string | undefined)[];
}

export function getMessage(validationMessages: ValidationMessages | undefined, key: string) {
    if (!validationMessages) {
        return "";
    }
    const messageObject = validationMessages[key];
    if (messageObject) {
        const messages = messageObject.filter(v => v != undefined);
        if (messages.length > 0) {
            return messages[0];
        }
    }
    return "";
}

export function validate(validationMessages: ValidationMessages) {
    let pass = true;
    Object.keys(validationMessages).map(key => {
        if (validationMessages[key].filter(v => v != undefined).length > 0) {
            pass = false;
            return;
        }
    });
    return pass;
}

export function numberMax(target: number, max: number, message: string) {
    if (!target) {
        return undefined;
    }
    if (target > max) {
        return message;
    }
    return undefined;
}

export function numberMin(target: number, min: number, message: string) {
    if (!target) {
        return undefined;
    }
    if (target > min) {
        return message;
    }
    return undefined;
}

export function numberRange(target: number, min: number, max: number, message: string) {
    if (!target) {
        return undefined;
    }
    if (target > max || target < min) {
        return message;
    }
    return undefined;
}

export function required(target: any, message: string) {
    if (target != undefined) {
        if (typeof target == "string") {
            if (target != "" && target != undefined && target != null) {
                return undefined;
            }
            return message;
        }
        else {
            return undefined
        }
    }
    return message;
}

export function maxLength(target: any, maxLength: number, message: string) {
    if (target == undefined) {
        return undefined;
    }
    if (typeof target == "string") {
        if (target.length > maxLength) {
            return message;
        }
        return undefined
    }
}

export function minLength(target: any, minLength: number, message: string) {
    if (target == undefined) {
        return undefined;
    }
    if (typeof target == "string") {
        if (target.length < minLength) {
            return message;
        }
        return undefined
    }
}

export function regularExpression(target: string, expression: string, message: string) {
    if (target == undefined || target == "" || target == null) {
        return undefined;
    }
    if (target.match(expression)) {
        return undefined;
    }
    return message;
}

export function maxDate(target: Date, maxDate: Date, message: string) {
    if (target == undefined) {
        return undefined;
    }
    if (target > maxDate) {
        return message;
    }
    return undefined
}

export function minDate(target: Date, minDate: Date, message: string) {
    if (target == undefined) {
        return undefined;
    }
    if (target < minDate) {
        return message;
    }
    return undefined
}

export function maxFileSize(target: File, maxMB: number, message: string) {
    if (!target) {
        return undefined;
    }
    if (target.size > maxMB * 1024 * 1024) {
        return message;
    }
    return undefined;
}

export function minFileSize(target: File, minMB: number, message: string) {
    if (!target) {
        return undefined;
    }
    if (target.size < minMB * 1024 * 1024) {
        return message;
    }
    return undefined;
}

export function emailAddress(email: string, message: string) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!email) {
        return undefined;
    }
    if (emailRegex.test(email)) {
        return undefined;
    }
    return message;
}

export function numberString(str: string, message: string) {
    if (!str) {
        return undefined;
    }
    const numberRegex = /^\d+$/;
    if (numberRegex.test(str)) {
        return undefined;
    }
    return message;
}

