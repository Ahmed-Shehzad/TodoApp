import dayjs from "dayjs";

export function DateTimeToFormattedString(
  isoDateTime?: string | number | Date | dayjs.Dayjs | null
) {
  // Parse the ISO datetime string using Day.js
  const parsedDateTime = dayjs(isoDateTime);
  // Format the datetime string according to the desired format
  const formattedDateTime = parsedDateTime.format("DD/MM/YYYY HH:mm:ss");
  return formattedDateTime;
}
