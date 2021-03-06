// Generated by typings
// Source: https://raw.githubusercontent.com/DefinitelyTyped/DefinitelyTyped/7de6c3dd94feaeb21f20054b9f30d5dabc5efabd/jsnlog/jsnlog.d.ts
declare namespace JSNLog {

	interface JSNLogOptions {
		enabled?: boolean;
		maxMessages?: number;
		defaultAjaxUrl?: string;
		clientIP?: string;
		requestId?: string;
		defaultBeforeSend?: (xhr: XMLHttpRequest) => void;
	}

	interface JSNLogFilterOptions {
		level?: number;
		ipRegex?: string;
		userAgentRegex?: string;
		disallow?: string;
	}

	interface JSNLogLoggerOptions extends JSNLogFilterOptions {
		appenders?: JSNLogAppender[];
		onceOnly?: string[];
	}

	// Base for all appender options types
	interface JSNLogAppenderOptions extends JSNLogFilterOptions {
		sendWithBufferLevel?: number;
		storeInBufferLevel?: number;
		bufferSize?: number;
		batchSize?: number;
	}

	interface JSNLogAjaxAppenderOptions extends JSNLogAppenderOptions {
		url?: string;
		beforeSend?: (xhr: XMLHttpRequest) => void;
	}

	interface JSNLogLogger {
		setOptions(options: JSNLogLoggerOptions): JSNLogLogger;

		trace(logObject: any): JSNLogLogger;
		debug(logObject: any): JSNLogLogger;
		info(logObject: any): JSNLogLogger;
		warn(logObject: any): JSNLogLogger;
		error(logObject: any): JSNLogLogger;
		fatal(logObject: any): JSNLogLogger;
		fatalException(logObject: any, e: any): JSNLogLogger;
		log(level: number, logObject: any, e?: any): JSNLogLogger;
	}

	interface JSNLogAppender {
		setOptions(options: JSNLogAppenderOptions): JSNLogAppender;
	}

	interface JSNLogAjaxAppender extends JSNLogAppender {
		setOptions(options: JSNLogAjaxAppenderOptions): JSNLogAjaxAppender;
	}

	interface JSNLogConsoleAppender extends JSNLogAppender {
	}

	interface JSNLogStatic {
		(loggerName?: string): JSNLogLogger;

		setOptions(options: JSNLogOptions): JSNLogStatic;
		createAjaxAppender(appenderName: string): JSNLogAjaxAppender;
		createConsoleAppender(appenderName: string): JSNLogConsoleAppender;

		getTraceLevel(): number;
		getDebugLevel(): number;
		getInfoLevel(): number;
		getWarnLevel(): number;
		getErrorLevel(): number;
		getFatalLevel(): number;
	}
}

declare function __jsnlog_configure(jsnlog: JSNLog.JSNLogStatic): void;

﻿
// Ambient declaration of the JL object itself

declare var JL: JSNLog.JSNLogStatic;