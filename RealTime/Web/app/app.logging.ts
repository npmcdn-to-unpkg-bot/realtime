namespace App
{
    var app = angular.module("app");

    app.config(config);

    config.$inject = ["$provide"];
    function config($provide): void {

        var jsnlog: JSNLog.JSNLogLogger = JL("Angular");

        $provide.decorator("$log", ["$delegate",
            $delegate => {
                return new Logger(jsnlog, $delegate);
            }
        ]);

        $provide.decorator("$exceptionHandler", ["$delegate",
            $delegate => (exception, cause) => {
                $delegate(exception, cause);

                // window.location.href = "/Error";
                window.alert(exception);
            }
        ]);
    }

    class Logger {
        constructor(private logger: JSNLog.JSNLogLogger, private originalLogger: angular.ILogService) {
        }

        debug(args) {
            this.logger.debug(args);
            if (this.originalLogger) {
                this.originalLogger.debug(args);
            }
        }

        info(args) {
            this.logger.info(args);
            if (this.originalLogger) {
                this.originalLogger.info(args);
            }
        }

        warn(args) {
            this.logger.warn(args);
            if (this.originalLogger) {
                this.originalLogger.warn(args);
            }
        }

        error(args) {
            this.logger.error(args);
            if (this.originalLogger) {
                this.originalLogger.error(args);
            }
        }

        log(args) {
            this.logger.trace(args);
            if (this.originalLogger) {
                this.originalLogger.log(args);
            }
        }
    }
}
