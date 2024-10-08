// TypeScript type definitions generated from Eldergrove Engine
declare const exports: any;
declare function log_info(message: string, args: any): void;
declare function log_debug(message: string, args: any): void;
declare function log_warn(message: string, args: any): void;
declare function log_error(message: string, args: any): void;
declare function add_ctx_var(name: string, value: any): void;
declare function action_register_cmd(command: string, action: () => void): void;
declare function action_unregister_cmd(command: string): void;
declare function action_execute_cmd(command: string): void;
declare function name_add(type: string, name: string): void;
declare function name_add_from_file(type: string, path: string): void;
declare function name_generate(type: string): string;
declare function add_color(colorName: string, value: any): void;
/** Returns a random integer value. */
declare function random_int(min: number, max: number): number;
/** Rolls a dice expression. */
declare function random_dice(expression: string): number;
/** Returns a random boolean value. */
declare function random_bool(): boolean;
declare function on_engine_start(action: () => void): void;
declare function dispatch_event(eventName: string, eventData: any): void;
declare function subscribe_to_event(eventName: string, eventHandler: any): void;
declare function add_tile(tile: ITileEntry): void;

declare interface ITileEntry {
    id: string;
    tags: string[];
    symbol: string;
    foreground: string?;
    background: string?;
    is_blocking: boolean;
    is_transparent: boolean;
}