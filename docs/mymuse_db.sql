INSERT INTO "Artists" ("Id", "Name",  "ImageUrl") VALUES
	(2, 'Neil Fernandes', 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/artists/2_avatar.webp'),
	(3, 'eddy', 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/artists/3_avatar.webp'),
	(4, 'Igor Lisul', 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/artists/4_avatar.webp');

INSERT INTO "Albums" ("Id", "Title", "ArtistId", "ImageUrl", "ReleaseDate") VALUES
	(1, 'Salon Hang', 2, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/albums/1_image.webp', '2026-08-21'),
	(3,'2 Damn Loud', 3, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/albums/3_image.webp', '2018-09-19'),
	(4,'The Oasis of Peace', 4, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/albums/4_image.webp', '2022-05-06');

INSERT INTO "Songs" ("Id", "Title", "AlbumId", "ArtistId", "AudioUrl", "Duration", "ReleaseDate", "IsSingle", "PlaysNumber", "ImageUrl") VALUES 
    (2, 'Life Goes On', 1, 2, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/2_audio.mp3', 260, '2026-08-21', FALSE, 1300, NULL),
    (3, 'Big Fish (Swimming In A Little Pond)', 1, 2, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/3_audio.mp3', 153, '2026-08-21', FALSE, 863, NULL),
    (4, 'Holiday Dreaming', 1, 2, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/4_audio.mp3', 166, '2026-08-21', FALSE, 702, NULL),
    (5, 'New Year''s Resolution', 1, 2, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/5_audio.mp3', 266, '2026-08-21', FALSE, 752, NULL),
    (6, 'That''s What Love Can Be', 1, 2, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/6_audio.mp3', 374, '2026-08-21', FALSE, 584, NULL),
    (7, 'Pure Adrenaline', 3, 3, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/7_audio.mp3', 33, '2018-09-19', FALSE, 63523, NULL),
    (8, 'Ruff', 3, 3, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/8_audio.mp3', 46, '2018-09-19', FALSE, 18712, NULL),
    (9, 'All The Way Up', 3, 3, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/9_audio.mp3', 161, '2018-09-19', FALSE, 25400, NULL),
    (10, 'Machinery', 3, 3, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/10_audio.mp3', 221, '2018-09-19', FALSE, 14654, NULL),
    (11, 'Keep It in Your Heart', 4, 4, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/11_audio.mp3', 203, '2022-05-06', FALSE, 815, NULL),
    (12, 'Holding Your Hand', 4, 4, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/12_audio.mp3', 202, '2022-05-06', FALSE, 552, NULL),
    (13, 'Raindrops', 4, 4, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/13_audio.mp3', 225, '2022-05-06', FALSE, 745, NULL),
    (14, 'Always Together', 4, 4, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/14_audio.mp3', 200, '2022-05-06', FALSE, 482, NULL),
    (15, 'Forest Morning Ambience', 4, 4, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/15_audio.mp3', 190, '2022-05-06', FALSE, 512, NULL),
    (17, 'Distant Horizon', 4, 4, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/17_audio.mp3', 208, '2022-05-06', FALSE, 387, NULL),
    (18, 'Dreams of the Celts', 4, 4, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/18_audio.mp3', 187, '2022-05-06', FALSE, 476, NULL),
    (19, 'On a Lonely Day', 4, 4, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/19_audio.mp3', 196, '2022-05-06', FALSE, 432, NULL),
    (20, 'Sailor''s Song', 4, 4, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/20_audio.mp3', 218, '2022-05-06', FALSE, 650, NULL),
    (16, 'Heart Knows', 4, 4, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/16_audio.mp3', 228, '2022-05-06', FALSE, 915, NULL),
    (21, 'Blues Musing', NULL, 2, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/21_audio.mp3', 295, '2026-09-11', TRUE, 981, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/21_image.webp'),
    (24, 'August', NULL, 2, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/24_audio.mp3', 200, '2026-09-03', TRUE, 520, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/24_image.webp'),
    (25, 'Deep Diver', NULL, 2, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/25_audio.mp3', 259, '2026-08-31', TRUE, 1124, 'https://pub-10d532d8f2864c1f9c3ea9a78de4e84b.r2.dev/songs/25_image.webp');