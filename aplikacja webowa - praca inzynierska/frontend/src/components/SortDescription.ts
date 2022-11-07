export type SortBy = 'date';
export type SortDirection = 'asc'|'desc';
export interface SortDescription 
{
    by: SortBy;
    direction: SortDirection;
};